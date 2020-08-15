namespace DotNetNuke.Web.Client.ClientResourceManagement
{
    using System;
    using System.Web.UI;

    using DotNetNuke.Abstractions;
    using DotNetNuke.Abstractions.Clients.ClientResourceManagement;

    using NewFileOrder = DotNetNuke.Abstractions.Clients.FileOrder;

    public partial class ClientResourceManager : IClientResourceManager
    {
        static IClientResourceManager _instance = new ClientResourceManager();

        /// <summary>
        /// Adds the neccessary configuration to website root web.config to use the Client Depenedecny componenet.
        /// </summary>
        public static void AddConfiguration() => 
            _instance.AddConfiguration();

        public static bool IsInstalled() => 
            _instance.IsInstalled();

        public static void RegisterAdminStylesheet(Page page, string filePath) => 
            _instance.RegisterAdminStylesheet((IDnnPage)page, filePath);

        public static void RegisterDefaultStylesheet(Page page, string filePath) => 
            _instance.RegisterDefaultStylesheet((IDnnPage)page, filePath);

        public static void RegisterFeatureStylesheet(Page page, string filePath) => 
            _instance.RegisterFeatureStylesheet((IDnnPage)page, filePath);

        public static void RegisterIEStylesheet(Page page, string filePath) => 
            _instance.RegisterIEStylesheet((IDnnPage)page, filePath);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        public static void RegisterScript(Page page, string filePath) => 
            _instance.RegisterScript((IDnnPage)page, filePath);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        public static void RegisterScript(Page page, string filePath, int priority) => 
            _instance.RegisterScript((IDnnPage)page, filePath, priority);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        public static void RegisterScript(Page page, string filePath, FileOrder.Js priority) => 
            _instance.RegisterScript((IDnnPage)page, filePath, (NewFileOrder.Js)priority);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        public static void RegisterScript(Page page, string filePath, FileOrder.Js priority, string provider) =>
            _instance.RegisterScript((IDnnPage)page, filePath, (NewFileOrder.Js)priority, provider);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        public static void RegisterScript(Page page, string filePath, int priority, string provider) =>
            _instance.RegisterScript((IDnnPage)page, filePath, priority, provider);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        /// <param name="name">Name of framework like Bootstrap, Angular, etc</param>
        /// <param name="version">Version nr of framework</param>
        public static void RegisterScript(Page page, string filePath, int priority, string provider, string name, string version) =>
            _instance.RegisterScript((IDnnPage)page, filePath, priority, provider, name, version);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        public static void RegisterStyleSheet(Page page, string filePath) =>
            _instance.RegisterStyleSheet((IDnnPage)page, filePath);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Defaults to rendering in the page header.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        public static void RegisterStyleSheet(Page page, string filePath, int priority) =>
            _instance.RegisterStyleSheet((IDnnPage)page, filePath, priority);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Defaults to rendering in the page header.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        public static void RegisterStyleSheet(Page page, string filePath, FileOrder.Css priority) =>
            _instance.RegisterStyleSheet((IDnnPage)page, filePath, (NewFileOrder.Css)priority);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Allows for overriding the default provider.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The provider name to be used to render the css file on the page.</param>
        public static void RegisterStyleSheet(Page page, string filePath, int priority, string provider) =>
            _instance.RegisterStyleSheet((IDnnPage)page, filePath, priority, provider);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Allows for overriding the default provider.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The provider name to be used to render the css file on the page.</param>
        /// <param name="name">Name of framework like Bootstrap, Angular, etc</param>
        /// <param name="version">Version nr of framework</param>
        public static void RegisterStyleSheet(Page page, string filePath, int priority, string provider, string name, string version) =>
            _instance.RegisterStyleSheet((IDnnPage)page, filePath, priority, provider, name, version);

        /// <summary>
        /// This is a utility method that can be called to update the version of the composite files.
        /// </summary>
        [Obsolete("This method is not required anymore. The CRM vesion is now managed in host settings and site settings.. Scheduled removal in v11.0.0.")]
        public static void UpdateVersion()
        {

        }

        /// <summary>
        /// Clear the default compisite files so that it can be generated next time.
        /// </summary>
        public static void ClearCache() =>
            _instance.ClearCache();

        public static void ClearFileExistsCache(string path) =>
            _instance.ClearFileExistsCache(path);

        public static void EnableAsyncPostBackHandler() =>
            _instance.EnableAsyncPostBackHandler();
    }
}
