using System.Web;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Web.Mvc.RazorPages
{
    public interface ITabAndModuleInfoProvider
    {
        bool TryFindTabId(HttpRequestBase request, out int tabId);
        bool TryFindModuleId(HttpRequestBase request, out int moduleId);
        bool TryFindModuleInfo(HttpRequestBase request, out ModuleInfo moduleInfo);
    }
}