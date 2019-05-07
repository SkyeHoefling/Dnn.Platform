using DotNetNuke.Library.Contracts.Security.Permissions;
using System.Collections.Generic;

namespace DotNetNuke.Library.Contracts.Entities.Modules.Definitions
{
    public interface IModuleDefinitionInfo
    {
        int ModuleDefID { get; set; }
        int DefaultCacheTime { get; set; }
        int DesktopModuleID { get; set; }
        string FriendlyName { get; set; }
        string DefinitionName { get; set; }
        Dictionary<string, IModuleControlInfo> ModuleControls { get; }
        Dictionary<string, IPermissionInfo> Permissions { get; }
        void LoadControls();
    }
}
