using System.Web.UI;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    public interface IHostControlBuilder
    {
        Control Build(string controlKey);
        Control Build(string controlKey, string controlSrc);
    }
}
