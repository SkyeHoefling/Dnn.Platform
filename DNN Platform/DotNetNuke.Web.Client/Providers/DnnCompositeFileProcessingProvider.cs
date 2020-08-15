// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System;
using System.IO;

namespace DotNetNuke.Web.Client.Providers
{
    using ClientDependency.Core;
    using ClientDependency.Core.CompositeFiles;
    using ClientDependency.Core.CompositeFiles.Providers;
    using System.Collections.Generic;
    
    using IDnnCompositeFileProcessingProvider = DotNetNuke.Abstractions.Clients.ClientResourceManagement.ICompositeFileProcessingProvider;
    using IDnnClientDependencyFile = DotNetNuke.Abstractions.Clients.ClientResourceManagement.IClientDependencyFile;
    using DnnClientDependencyType = DotNetNuke.Abstractions.Clients.ClientResourceManagement.ClientDependencyType;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// A provider for combining, minifying, compressing and saving composite scripts/css files
    /// </summary>
    public class DnnCompositeFileProcessingProvider : CompositeFileProcessingProvider, IDnnCompositeFileProcessingProvider
    {
        private readonly ClientResourceSettings clientResourceSettings = new ClientResourceSettings();

        public override string MinifyFile(Stream fileStream, ClientDependency.Core.ClientDependencyType type)
        {
            Func<Stream, string> streamToString = stream =>
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Cannot read input stream");

                if (stream.CanSeek)
                    stream.Position = 0;

                var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            };

            switch (type)
            {
                case ClientDependency.Core.ClientDependencyType.Css:
                    return MinifyCss ? CssHelper.MinifyCss(fileStream) : streamToString(fileStream);
                case ClientDependency.Core.ClientDependencyType.Javascript:
                    return MinifyJs ? JSMin.CompressJS(fileStream) : streamToString(fileStream);
                default:
                    return streamToString(fileStream);
            }
        }

        public override string MinifyFile(string fileContents, ClientDependency.Core.ClientDependencyType type)
        {
            switch (type)
            {
                case ClientDependency.Core.ClientDependencyType.Css:
                    return MinifyCss ? CssHelper.MinifyCss(fileContents) : fileContents;
                case ClientDependency.Core.ClientDependencyType.Javascript:
                {
                    if (!MinifyJs)
                        return fileContents;

                    using (var ms = new MemoryStream())
                    using (var writer = new StreamWriter(ms))
                    {
                        writer.Write(fileContents);
                        writer.Flush();
                        return JSMin.CompressJS(ms);
                    }
                }
                default:
                    return fileContents;
            }
        }

        private bool MinifyCss
        {
            get
            {
                var enableCssMinification = clientResourceSettings.EnableCssMinification();
                return enableCssMinification.HasValue ? enableCssMinification.Value : EnableCssMinify;
            }
        }

        private bool MinifyJs
        {
            get
            {
                var enableJsMinification = clientResourceSettings.EnableJsMinification();
                return enableJsMinification.HasValue ? enableJsMinification.Value : EnableJsMinify;
            }
        }

        IEnumerable<string> IDnnCompositeFileProcessingProvider.ProcessCompositeList<THttpContext>(IEnumerable<IDnnClientDependencyFile> dependencies, DnnClientDependencyType type, THttpContext http)
        {
            var legacyDependencies = dependencies?.Where(dependency => dependencies is IClientDependencyFile).Cast<IClientDependencyFile>();
            if (!(http is HttpContextBase) || legacyDependencies == null || !legacyDependencies.Any())
                return new string[0];

            return ProcessCompositeList(legacyDependencies, (ClientDependencyType)type, http as HttpContextBase);
        }
    }
}
