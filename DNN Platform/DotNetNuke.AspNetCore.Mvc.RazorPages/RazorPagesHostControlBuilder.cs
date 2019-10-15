using System.Web.UI;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    internal class RazorPagesHostControlBuilder : IHostControlBuilder
    {
        public IHostControl HostControl { get; set; }
        public Control Build(string controlKey)
        {
            HostControl.ControlKey = $"~/{controlKey}";
            return HostControl.ToControl();
        }

        public Control Build(string controlKey, string controlSrc)
        {
            return Build(controlKey);
        }
    }
}
