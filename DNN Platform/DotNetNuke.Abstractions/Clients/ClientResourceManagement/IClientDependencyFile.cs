﻿namespace DotNetNuke.Abstractions.Clients.ClientResourceManagement
{
    public interface IClientDependencyFile
    {
        string FilePath { get; set; }
        ClientDependencyType DependencyType { get; }
        int Priority { get; set; }
        int Group { get; set; }
        string PathNameAlias { get; set; }
        string ForceProvider { get; set; }
        bool ForceBundle { get; set; }
        string Name { get; set; }
        string Version { get; set; }
        bool ForceVersion { get; set; }
    }
}
