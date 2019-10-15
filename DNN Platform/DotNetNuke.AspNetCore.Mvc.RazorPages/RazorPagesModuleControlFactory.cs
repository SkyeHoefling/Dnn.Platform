using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;
using System.Web.UI;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{


    public class RazorPagesModuleControlFactory : BaseModuleControlFactory
    {
        protected IHostControlBuilder HostControlBuilder { get; }
        public RazorPagesModuleControlFactory(IHostControlBuilder hostControlBuilder)
        {
            HostControlBuilder = hostControlBuilder;
        }

        public override Control CreateControl(TemplateControl containerControl, string controlKey, string controlSrc)
        {
            return HostControlBuilder.Build(controlKey, controlSrc);
        }

        public override Control CreateModuleControl(TemplateControl containerControl, ModuleInfo moduleConfiguration)
        {
            return CreateControl(containerControl, string.Empty, moduleConfiguration.ModuleControl.ControlSrc);
        }

        public override Control CreateSettingsControl(TemplateControl containerControl, ModuleInfo moduleConfiguration, string controlSrc)
        {
            return CreateControl(containerControl, string.Empty, controlSrc);
        }
    }
}
