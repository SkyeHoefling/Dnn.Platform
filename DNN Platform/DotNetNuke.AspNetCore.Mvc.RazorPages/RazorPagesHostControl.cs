using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.UI.Modules;
using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    public interface IHostControl
    {
        string ControlKey { get; set; }
        Control ToControl();
    }

    public class RazorPagesHostControl : ModuleControlBase, IActionable, IHostControl
    {
        protected IEngine Engine { get; }
        public RazorPagesHostControl(IEngine engine)
        {
            Engine = engine;
        }

        public string ControlKey { get; set; }
        public ModuleActionCollection ModuleActions => throw new NotImplementedException();

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(ControlKey))
            {
                var writer = new StringWriter();
                Engine.Render(ControlKey, ModuleContext, LocalResourceFile, writer);
                Controls.Add(new LiteralControl(HttpUtility.HtmlDecode(writer.ToString())));
            }
        }

        public Control ToControl()
        {
            return this;
        }
    }
}
