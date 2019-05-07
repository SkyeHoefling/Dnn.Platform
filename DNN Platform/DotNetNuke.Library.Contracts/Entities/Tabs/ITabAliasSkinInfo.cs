using System.Data;

namespace DotNetNuke.Library.Contracts.Entities.Tabs
{
    public interface ITabAliasSkinInfo
    {
        int TabAliasSkinId { get; set; }
        string HttpAlias { get; set; }
        int PortalAliasId { get; set; }
        string SkinSrc { get; set; }
        int TabId { get; set; }
        int KeyID { get; set; }
        void Fill(IDataReader dr);
    }
}
