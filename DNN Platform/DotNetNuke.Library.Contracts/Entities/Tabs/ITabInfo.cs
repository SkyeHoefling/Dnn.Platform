using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Library.Contracts.Security.Permissions;
using DotNetNuke.Library.Contracts.Entities.Modules;
using DotNetNuke.Library.Contracts.Entities.Content;

namespace DotNetNuke.Library.Contracts.Entities.Tabs
{
    public interface ITabInfo : IContentItem
    {
        ArrayList BreadCrumbs { get; set; }
        string ContainerPath { get; set; }
        string ContainerSrc { get; set; }
        string CultureCode { get; set; }
        Guid DefaultLanguageGuid { get; set; }
        string Description { get; set; }
        bool DisableLink { get; set; }
        DateTime EndDate { get; set; }
        bool HasChildren { get; set; }
        string IconFileRaw { get; }
        string IconFileLargeRaw { get; }
        bool IsDeleted { get; set; }
        bool IsSecure { get; set; }
        bool IsVisible { get; set; }
        bool IsSystem { get; set; }
        bool HasBeenPublished { get; set; }
        bool HasAVisibleVersion { get; }
        string KeyWords { get; set; }
        int Level { get; set; }
        Guid LocalizedVersionGuid { get; set; }
        ArrayList Modules { get; set; }
        string PageHeadText { get; set; }
        ArrayList Panes { get; }
        int ParentId { get; set; }
        bool PermanentRedirect { get; set; }
        int PortalID { get; set; }
        int RefreshInterval { get; set; }
        float SiteMapPriority { get; set; }
        string SkinPath { get; set; }
        string SkinSrc { get; set; }
        DateTime StartDate { get; set; }
        string TabName { get; set; }
        int TabOrder { get; set; }
        string TabPath { get; set; }
        string Title { get; set; }
        Guid UniqueId { get; set; }
        Guid VersionGuid { get; set; }
        Dictionary<int, IModuleInfo> ChildModules { get; }
        ITabInfo DefaultLanguageTab { get; }
        bool DoNotRedirect { get; }
        string IconFile { get; set; }
        string IconFileLarge { get; set; }
        string IndentedTabName { get; }
        bool IsDefaultLanguage { get; }
        bool IsNeutralCulture { get; }
        bool IsSuperTab { get; set; }
        bool IsTranslated { get; }
        int KeyID { get; set; }
        string LocalizedTabName { get; }
        Dictionary<string, ITabInfo> LocalizedTabs { get; }
        string SkinDoctype { get; set; }
        ITabPermissionCollection TabPermissions { get; }
        Hashtable TabSettings { get; }
        TabType TabType { get; }
        List<ITabAliasSkinInfo> AliasSkins { get; }
        Dictionary<string, string> CustomAliases { get; }
        string FullUrl { get; }
        bool TabPermissionsSpecified { get; }
        List<ITabUrlInfo> TabUrls { get; }
        string Url { get; set; }
        bool UseBaseFriendlyUrls { get; set; }
        ITabInfo Clone();
        void Fill(IDataReader dr);
        string GetCurrentUrl(string cultureCode);
    }
}
