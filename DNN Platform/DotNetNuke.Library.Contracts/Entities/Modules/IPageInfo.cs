using System.Xml;
using System.Xml.Schema;

namespace DotNetNuke.Library.Contracts.Entities.Modules
{
    public interface IPageInfo
    {
        string Type { get; set; }
        bool IsCommon { get; set; }
        string Name { get; set; }
        string Icon { get; set; }
        string LargeIcon { get; set; }
        string Description { get; set; }
        bool HasAdminPage();
        bool HasHostPage();
        XmlSchema GetSchema();
        void ReadXml(XmlReader reader);
        void WriteXml(XmlWriter writer);
    }
}
