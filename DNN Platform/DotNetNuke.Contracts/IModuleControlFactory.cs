using System.Web.UI;

namespace DotNetNuke.Contracts
{
    public interface IModuleControlFactory
    {
        Control CreateControl(TemplateControl containerControl, string controlKey, string controlSrc);

        Control CreateModuleControl(TemplateControl containerControl, IModuleInfo moduleConfiguration);

        Control CreateSettingsControl(TemplateControl containerControl, IModuleInfo moduleConfiguration, string controlSrc);
    }
}
