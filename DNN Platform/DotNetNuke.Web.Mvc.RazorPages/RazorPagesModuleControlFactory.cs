using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;
using System;
using System.Web.UI;

namespace DotNetNuke.Web.Mvc.RazorPages
{
    public class RazorPagesModuleControlFactory : IModuleControlFactory
    {
        public Control CreateControl(TemplateControl containerControl, string controlKey, string controlSrc)
        {
            return new Razor.RazorHostControl("~/" + controlSrc);
        }

        public Control CreateModuleControl(TemplateControl containerControl, ModuleInfo moduleConfiguration)
        {
            return CreateControl(containerControl, String.Empty, moduleConfiguration.ModuleControl.ControlSrc);
        }

        public Control CreateSettingsControl(TemplateControl containerControl, ModuleInfo moduleConfiguration, string controlSrc)
        {
            return CreateControl(containerControl, String.Empty, controlSrc);
        }
    }
}
