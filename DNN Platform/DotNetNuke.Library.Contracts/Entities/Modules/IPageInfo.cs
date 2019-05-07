using DotNetNuke.Library.Contracts.Entities.Modules.Definitions;
using System.Collections.Generic;

namespace DotNetNuke.Library.Contracts.Entities.Modules
{
    public interface IPageInfo
    {
        int DesktopModuleID { get; set; }
        int PackageID { get; set; }
        string AdminPage { get; set; }
        string BusinessControllerClass { get; set; }
        string Category { get; set; }
        string CodeSubDirectory { get; set; }
        string CompatibleVersions { get; set; }
        string Dependencies { get; set; }
        string Description { get; set; }
        string FolderName { get; set; }
        string FriendlyName { get; set; }
        string HostPage { get; set; }
        bool IsAdmin { get; set; }
        bool IsPortable { get; set; }
        bool IsPremium { get; set; }
        bool IsSearchable { get; set; }
        bool IsUpgradeable { get; set; }
        ModuleSharing Shareable { get; set; }
        Dictionary<string, IModuleDefinitionInfo> ModuleDefinitions { get; }
        string ModuleName { get; set; }
        string Permissions { get; set; }
        int SupportedFeatures { get; set; }
        string Version { get; set; }
        IPageInfo Page { get; set; }
    }
}
