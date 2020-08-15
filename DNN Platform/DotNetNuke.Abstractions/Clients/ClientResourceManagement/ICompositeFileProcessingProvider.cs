namespace DotNetNuke.Abstractions.Clients.ClientResourceManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    public interface ICompositeFileProcessingProvider
    {
        DirectoryInfo CompositeFilePath { get; }
        IEnumerable<string> ProcessCompositeList<THttpContext>(IEnumerable<IClientDependencyFile> dependencies, ClientDependencyType type, THttpContext http) where THttpContext : class;
    }
}
