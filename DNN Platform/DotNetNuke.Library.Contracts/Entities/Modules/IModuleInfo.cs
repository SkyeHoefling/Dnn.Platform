using DotNetNuke.Library.Contracts.Entities.Modules.Definitions;
using DotNetNuke.Library.Contracts.Entities.Tabs;
using DotNetNuke.Library.Contracts.Security.Permissions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetNuke.Library.Contracts.Entities.Modules
{
    public interface IModuleInfo
    {
        string Alignment { get; set; }
        bool AllModules { get; set; }
        bool AllTabs { get; set; }
        string Border { get; set; }
        string CacheMethod { get; set; }
        int CacheTime { get; set; }
        string Color { get; set; }
        string ContainerPath { get; set; }
        string ContainerSrc { get; set; }
        IDesktopModuleInfo DesktopModule { get; }
        int DesktopModuleID { get; set; }
        bool DisplayPrint { get; set; }
        bool DisplaySyndicate { get; set; }
        bool DisplayTitle { get; set; }
        DateTime EndDate { get; set; }
        string Footer { get; set; }
        string Header { get; set; }
        bool HideAdminBorder { get; }
        string IconFile { get; set; }
        bool InheritViewPermissions { get; set; }
        bool IsDefaultModule { get; set; }
        bool IsDeleted { get; set; }
        bool IsShareable { get; set; }
        bool IsShared { get; }
        bool IsShareableViewOnly { get; set; }
        bool IsWebSlice { get; set; }
        DateTime LastContentModifiedOnDate { get; set; }
        IModuleControlInfo ModuleControl { get; }
        int ModuleControlId { get; set; }
        int ModuleDefID { get; set; }
        IModuleDefinitionInfo ModuleDefinition { get; }
        int ModuleOrder { get; set; }
        IModulePermissionCollection ModulePermissions { get; set; }
        Hashtable ModuleSettings { get; }
        string ModuleTitle { get; set; }
        int ModuleVersion { get; set; }
        int OwnerPortalID { get; set; }
        int PaneModuleCount { get; set; }
        int PaneModuleIndex { get; set; }
        string PaneName { get; set; }
        int PortalID { get; set; }
        DateTime StartDate { get; set; }
        int TabModuleID { get; set; }
        Hashtable TabModuleSettings { get; }
        Guid UniqueId { get; set; }
        Guid VersionGuid { get; set; }
        VisibilityState Visibility { get; set; }
        DateTime WebSliceExpiryDate { get; set; }
        string WebSliceTitle { get; set; }
        int WebSliceTTL { get; set; }
        string CultureCode { get; set; }
        Guid DefaultLanguageGuid { get; set; }
        IModuleInfo DefaultLanguageModule { get; }
        bool IsDefaultLanguage { get; }
        bool IsLocalized { get; }
        bool IsNeutralCulture { get; }
        bool IsTranslated { get; }
        Dictionary<string, IModuleInfo> LocalizedModules { get; }
        Guid LocalizedVersionGuid { get; set; }
        ITabInfo ParentTab { get; }
    }
}