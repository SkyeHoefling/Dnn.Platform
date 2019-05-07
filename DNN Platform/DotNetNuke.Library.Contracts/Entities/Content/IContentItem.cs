using System.Collections.Specialized;

namespace DotNetNuke.Library.Contracts.Entities.Content
{
    public interface IContentItem
    {
        // todo - fix broken code
        int ContentItemId { get; set; }
        string Content { get; set; }
        string ContentKey { get; set; }
        int ContentTypeId { get; set; }
        bool Indexed { get; set; }
        NameValueCollection Metadata { get; }
        int ModuleID { get; set; }
        int TabID { get; set; }
        //public List<Term> Terms { get; }
        string ContentTitle { get; set; }
        //public List<IFileInfo> Files { get; }
        //public List<IFileInfo> Videos { get; }
        //public List<IFileInfo> Images { get; }
        int StateID { get; set; }
    }
}
