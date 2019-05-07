using DotNetNuke.Library.Contracts.Security;

namespace DotNetNuke.Library.Contracts.Entities.Modules
{
    public interface IModuleControlInfo
    {
        string ControlTitle { get; set; }
        SecurityAccessLevel ControlType { get; set; }
        string HelpURL { get; set; }
        string IconFile { get; set; }
        int ModuleControlID { get; set; }
        int ModuleDefID { get; set; }
        bool SupportsPopUps { get; set; }
        int ViewOrder { get; set; }
    }
}
