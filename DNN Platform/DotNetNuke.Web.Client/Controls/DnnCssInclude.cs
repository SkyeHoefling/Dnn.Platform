﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System.Web.UI;

namespace DotNetNuke.Web.Client.ClientResourceManagement
{
    using ClientDependency.Core.Controls;
    using DotNetNuke.Abstractions.Clients.ClientResourceManagement;

    /// <summary>
    /// Registers a CSS resource
    /// </summary>
    public class DnnCssInclude : CssInclude, IDnnInclude, IClientDependencyFile
    {
        public DnnCssInclude()
        {
            ForceProvider = ClientResourceManager.DefaultCssProvider;
        }

        ClientDependencyType IClientDependencyFile.DependencyType => (ClientDependencyType)DependencyType;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            this.PathNameAlias = this.PathNameAlias.ToLowerInvariant();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (AddTag || Context.IsDebuggingEnabled)
            {
                writer.Write("<!--CDF({0}|{1}|{2}|{3})-->", DependencyType, FilePath, ForceProvider, Priority);
            }
        }
    }
}
