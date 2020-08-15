// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
namespace DotNetNuke.Web.Client.ClientResourceManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;
    using System.Xml;

    using ClientDependency.Core;
    using ClientDependency.Core.CompositeFiles.Providers;
    using ClientDependency.Core.Config;

    using DotNetNuke.Abstractions;
    using DotNetNuke.Abstractions.Clients.ClientResourceManagement;
    using DotNetNuke.Instrumentation;

    using FileOrder = DotNetNuke.Abstractions.Clients.FileOrder;
    
    /// <summary>
    /// Provides the ability to request that client resources (JavaScript and CSS) be loaded on the client browser.
    /// </summary>
    public partial class ClientResourceManager : IClientResourceManager
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ClientResourceManager));
        internal const string DefaultCssProvider = "DnnPageHeaderProvider";
        internal const string DefaultJsProvider = "DnnBodyProvider";

        static Dictionary<string, bool> _fileExistsCache = new Dictionary<string, bool>();
        static ReaderWriterLockSlim _lockFileExistsCache = new ReaderWriterLockSlim();

        #region Private Methods

        private bool FileExists(IDnnPage page, string filePath)
        {
            // remove query string for the file exists check, won't impact the absoluteness, so just do it either way.
            filePath = RemoveQueryString(filePath);
            var cacheKey = filePath.ToLowerInvariant();
            // cache css file paths
            if (!_fileExistsCache.ContainsKey(cacheKey))
            {
                // appply lock after IF, locking is more expensive than worst case scenario (check disk twice)
                _lockFileExistsCache.EnterWriteLock();
                try
                {
                    _fileExistsCache[cacheKey] = IsAbsoluteUrl(filePath) || File.Exists(page.Server.MapPath(filePath));
                }
                finally
                {
                    _lockFileExistsCache.ExitWriteLock();
                }
            }

            // return if file exists from cache
            _lockFileExistsCache.EnterReadLock();
            try
            {
                return _fileExistsCache[cacheKey];
            }
            finally
            {
                _lockFileExistsCache.ExitReadLock();
            }
        }

        private bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        private string RemoveQueryString(string filePath)
        {
            var queryStringPosition = filePath.IndexOf("?", StringComparison.Ordinal);
            return queryStringPosition != -1 ? filePath.Substring(0, queryStringPosition) : filePath;
        }

        #endregion

        #region Public Methods
        /// <inheritdoc />
        void IClientResourceManager.AddConfiguration()
        {
            var configPath = HostingEnvironment.MapPath("~/web.config");
            if (!String.IsNullOrEmpty(configPath))
            {
                var xmlDoc = new XmlDocument { XmlResolver = null };
                xmlDoc.Load(configPath);
                XmlDocumentFragment xmlFrag;

                // Config Sections
                var sectionsConfig = xmlDoc.DocumentElement.SelectSingleNode("configSections");
                if (sectionsConfig != null)
                {
                    var clientDependencySectionConfig = sectionsConfig.SelectSingleNode("section[@name='clientDependency']");
                    if (clientDependencySectionConfig == null)
                    {
                        xmlFrag = xmlDoc.CreateDocumentFragment();
                        xmlFrag.InnerXml = "<section name=\"clientDependency\" type=\"ClientDependency.Core.Config.ClientDependencySection, ClientDependency.Core\" requirePermission=\"false\" />";
                        xmlDoc.DocumentElement.SelectSingleNode("configSections").AppendChild(xmlFrag);
                    }
                }

                // Module Config
                var systemWebServerModulesConfig = xmlDoc.DocumentElement.SelectSingleNode("system.webServer/modules");
                if (systemWebServerModulesConfig != null)
                {
                    var moduleConfig = systemWebServerModulesConfig.SelectSingleNode("add[@name=\"ClientDependencyModule\"]");
                    if (moduleConfig == null)
                    {
                        xmlFrag = xmlDoc.CreateDocumentFragment();
                        xmlFrag.InnerXml = "<add name=\"ClientDependencyModule\" type=\"ClientDependency.Core.Module.ClientDependencyModule, ClientDependency.Core\"  preCondition=\"managedHandler\" />";
                        xmlDoc.DocumentElement.SelectSingleNode("system.webServer/modules").AppendChild(xmlFrag);
                    }
                }
                // Handler Config
                var systemWebServerHandlersConfig = xmlDoc.DocumentElement.SelectSingleNode("system.webServer/handlers");
                if (systemWebServerHandlersConfig != null)
                {
                    var handlerConfig = systemWebServerHandlersConfig.SelectSingleNode("add[@name=\"ClientDependencyHandler\"]");
                    if (handlerConfig == null)
                    {
                        xmlFrag = xmlDoc.CreateDocumentFragment();
                        xmlFrag.InnerXml = "<add name=\"ClientDependencyHandler\" verb=\"*\" path=\"DependencyHandler.axd\" type=\"ClientDependency.Core.CompositeFiles.CompositeDependencyHandler, ClientDependency.Core\" preCondition=\"integratedMode\" />";
                        xmlDoc.DocumentElement.SelectSingleNode("system.webServer/handlers").AppendChild(xmlFrag);
                    }
                }

                // HttpModules Config
                var systemWebServerHttpModulesConfig = xmlDoc.DocumentElement.SelectSingleNode("system.web/httpModules");
                if (systemWebServerHttpModulesConfig != null)
                {
                    var httpModuleConfig = systemWebServerHttpModulesConfig.SelectSingleNode("add[@name=\"ClientDependencyModule\"]");
                    if (httpModuleConfig == null)
                    {
                        xmlFrag = xmlDoc.CreateDocumentFragment();
                        xmlFrag.InnerXml = "<add name=\"ClientDependencyModule\" type=\"ClientDependency.Core.Module.ClientDependencyModule, ClientDependency.Core\" />";
                        xmlDoc.DocumentElement.SelectSingleNode("system.web/httpModules").AppendChild(xmlFrag);
                    }
                }
                // HttpHandler Config
                var systemWebServerHttpHandlersConfig = xmlDoc.DocumentElement.SelectSingleNode("system.web/httpHandlers");
                if (systemWebServerHttpHandlersConfig != null)
                {
                    var httpHandlerConfig = systemWebServerHttpHandlersConfig.SelectSingleNode("add[@type=\"ClientDependency.Core.CompositeFiles.CompositeDependencyHandler, ClientDependency.Core\"]");
                    if (httpHandlerConfig == null)
                    {
                        xmlFrag = xmlDoc.CreateDocumentFragment();
                        xmlFrag.InnerXml = "<add verb=\"*\" path=\"DependencyHandler.axd\" type=\"ClientDependency.Core.CompositeFiles.CompositeDependencyHandler, ClientDependency.Core\" />";
                        xmlDoc.DocumentElement.SelectSingleNode("system.web/httpHandlers").AppendChild(xmlFrag);
                    }
                }

                // ClientDependency Config
                var clientDependencyConfig = xmlDoc.DocumentElement.SelectSingleNode("clientDependency");
                if (clientDependencyConfig == null)
                {
                    xmlFrag = xmlDoc.CreateDocumentFragment();
                    xmlFrag.InnerXml = @"<clientDependency version=""0"" fileDependencyExtensions="".js,.css"">
                                            <fileRegistration defaultProvider=""DnnPageHeaderProvider"">
                                              <providers>
                                                <add name=""DnnBodyProvider"" type=""DotNetNuke.Web.Client.Providers.DnnBodyProvider, DotNetNuke.Web.Client"" enableCompositeFiles=""false"" />
                                                <add name=""DnnPageHeaderProvider"" type=""DotNetNuke.Web.Client.Providers.DnnPageHeaderProvider, DotNetNuke.Web.Client"" enableCompositeFiles=""false"" />
                                                <add name=""DnnFormBottomProvider"" type=""DotNetNuke.Web.Client.Providers.DnnFormBottomProvider, DotNetNuke.Web.Client"" enableCompositeFiles=""false"" />
                                                <add name=""PageHeaderProvider"" type=""ClientDependency.Core.FileRegistration.Providers.PageHeaderProvider, ClientDependency.Core"" enableCompositeFiles=""false""/>
                                                <add name=""LazyLoadProvider"" type=""ClientDependency.Core.FileRegistration.Providers.LazyLoadProvider, ClientDependency.Core"" enableCompositeFiles=""false""/>
                                                <add name=""LoaderControlProvider"" type=""ClientDependency.Core.FileRegistration.Providers.LoaderControlProvider, ClientDependency.Core"" enableCompositeFiles=""false""/>
                                              </providers>
                                            </fileRegistration>
                                            <compositeFiles defaultFileProcessingProvider=""DnnCompositeFileProcessor"" compositeFileHandlerPath=""~/DependencyHandler.axd"">
                                              <fileProcessingProviders>
                                                <!-- For webfarms update the urlType attribute to Base64QueryStrings, default setting is MappedId -->
                                                <add name=""DnnCompositeFileProcessor"" type=""DotNetNuke.Web.Client.Providers.DnnCompositeFileProcessingProvider, DotNetNuke.Web.Client"" enableCssMinify=""false"" enableJsMinify=""true"" persistFiles=""true"" compositeFilePath=""~/App_Data/ClientDependency"" bundleDomains="""" urlType=""MappedId"" />
                                              </fileProcessingProviders>
                                            </compositeFiles>
                                          </clientDependency>";

                    xmlDoc.DocumentElement.AppendChild(xmlFrag);
                }

                // Save Config
                xmlDoc.Save(configPath);
            }
        }

        /// <inheritdoc />
        bool IClientResourceManager.IsInstalled()
        {
            var configPath = HostingEnvironment.MapPath("~/web.config");
            var installed = false;

            if (!String.IsNullOrEmpty(configPath))
            {
                var xmlDoc = new XmlDocument { XmlResolver = null };
                xmlDoc.Load(configPath);

                // Config Sections
                var sectionsConfig = xmlDoc.DocumentElement.SelectSingleNode("configSections");
                if (sectionsConfig != null)
                {
                    var clientDependencySectionConfig = sectionsConfig.SelectSingleNode("section[@name='clientDependency']");
                    installed = clientDependencySectionConfig != null;
                }
            }

            return installed;
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterAdminStylesheet(IDnnPage page, string filePath)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, FileOrder.Css.AdminCss);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterDefaultStylesheet(IDnnPage page, string filePath)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, (int)FileOrder.Css.DefaultCss, DefaultCssProvider, "dnndefault", "7.0.0");
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterFeatureStylesheet(IDnnPage page, string filePath)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, FileOrder.Css.FeatureCss);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterIEStylesheet(IDnnPage page, string filePath)
        {
            var browser = HttpContext.Current.Request.Browser;
            if (browser.Browser == "Internet Explorer" || browser.Browser == "IE")
            {
                (this as IClientResourceManager).RegisterStyleSheet(page, filePath, FileOrder.Css.IeCss);
            }
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath)
        {
            (this as IClientResourceManager).RegisterScript(page, filePath, FileOrder.Js.DefaultPriority);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath, int priority)
        {
            (this as IClientResourceManager).RegisterScript(page, filePath, priority, DefaultJsProvider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath, FileOrder.Js priority)
        {
            (this as IClientResourceManager).RegisterScript(page, filePath, (int)priority, DefaultJsProvider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath, FileOrder.Js priority, string provider)
        {
            (this as IClientResourceManager).RegisterScript(page, filePath, (int)priority, provider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath, int priority, string provider)
        {
            (this as IClientResourceManager).RegisterScript(page, filePath, priority, provider, "", "");
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterScript(IDnnPage page, string filePath, int priority, string provider, string name, string version)
        {
            var include = new DnnJsInclude { ForceProvider = provider, Priority = priority, FilePath = filePath, Name = name, Version = version };
            page.AddInclude("ClientResourceIncludes", include);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterStyleSheet(IDnnPage page, string filePath)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, Constants.DefaultPriority, DefaultCssProvider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterStyleSheet(IDnnPage page, string filePath, int priority)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, priority, DefaultCssProvider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterStyleSheet(IDnnPage page, string filePath, FileOrder.Css priority)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, (int)priority, DefaultCssProvider);
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterStyleSheet(IDnnPage page, string filePath, int priority, string provider)
        {
            (this as IClientResourceManager).RegisterStyleSheet(page, filePath, priority, provider, "", "");
        }

        /// <inheritdoc />
        void IClientResourceManager.RegisterStyleSheet(IDnnPage page, string filePath, int priority, string provider, string name, string version)
        {
            var fileExists = false;

            // Some "legacy URLs" could be using their own query string versioning scheme (and we've forced them to use the new API through re-routing PageBase.(this as IClientResourceManager).RegisterStyleSheet
            // Ensure that physical CSS files with query strings have their query strings removed
            if (filePath.Contains(".css?"))
            {
                var filePathSansQueryString = RemoveQueryString(filePath);
                if (File.Exists(page.Server.MapPath(filePathSansQueryString)))
                {
                    fileExists = true;
                    filePath = filePathSansQueryString;
                }
            }
            else if (filePath.Contains("WebResource.axd"))
            {
                // here if we add async it should work
                fileExists = true;
            }

            if (fileExists || FileExists(page, filePath))
            {
                var include = new DnnCssInclude { ForceProvider = provider, Priority = priority, FilePath = filePath, Name = name, Version = version };
                page.AddInclude("ClientResourceIncludes", include);
            }
        }

        /// <inheritdoc />
        void IClientResourceManager.ClearCache()
        {
            var provider = ClientDependencySettings.Instance.DefaultCompositeFileProcessingProvider;
            if (provider is CompositeFileProcessingProvider)
            {
                try
                {
                    var folder = provider.CompositeFilePath;
                    if (folder.Exists)
                    {
                        var files = folder.GetFiles("*.cd?");
                        foreach (var file in files)
                        {
                            file.Delete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

            }
        }

        /// <inheritdoc />
        void IClientResourceManager.ClearFileExistsCache(string path)
        {
            _lockFileExistsCache.EnterWriteLock();
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    _fileExistsCache.Clear();
                }
                else
                {
                    _fileExistsCache.Remove(path.ToLowerInvariant());
                }
            }
            finally
            {
                _lockFileExistsCache.ExitWriteLock();
            }
        }

        /// <inheritdoc />
        void IClientResourceManager.EnableAsyncPostBackHandler()
        {
            if (HttpContext.Current != null && !HttpContext.Current.Items.Contains("AsyncPostBackHandlerEnabled"))
            {
                HttpContext.Current.Items.Add("AsyncPostBackHandlerEnabled", true);
            }
        }
        #endregion

    }
}
