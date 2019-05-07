using DotNetNuke.Library.Contracts.Entities.Modules;
#if NET472
using System.Web.UI;
#endif

namespace DotNetNuke.Library.Contracts.UI.Modules
{
    public interface IModuleControlFactory
    {
#if NET472
        Control CreateControl(TemplateControl containerControl, string controlKey, string controlSrc);

        Control CreateModuleControl(TemplateControl containerControl, IModuleInfo moduleConfiguration);

        Control CreateSettingsControl(TemplateControl containerControl, IModuleInfo moduleConfiguration, string controlSrc);
#endif
    }
}
