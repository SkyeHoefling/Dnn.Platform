// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
namespace DotNetNuke.Web.Client.ClientResourceManagement
{
    using ClientDependency.Core.Controls;
    using DotNetNuke.Abstractions.Clients;

    /// <summary>
    /// Represents an included client resource
    /// </summary>
    public class ClientResourceInclude : ClientDependencyInclude, IDnnInclude, IClientDependencyFile
    {
        ClientDependencyType IClientDependencyFile.DependencyType => (ClientDependencyType)DependencyType;
    }
}
