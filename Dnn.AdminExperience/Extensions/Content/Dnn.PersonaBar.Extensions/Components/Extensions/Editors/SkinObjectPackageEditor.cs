using System;
using Dnn.PersonaBar.Extensions.Components.Dto;
using Dnn.PersonaBar.Extensions.Components.Dto.Editors;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Logging;
using DotNetNuke.Services.Installer.Packages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.PersonaBar.Extensions.Components.Editors
{
    public class SkinObjectPackageEditor : IPackageEditor
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<SkinObjectPackageEditor>>();

        #region IPackageEditor Implementation

        public PackageInfoDto GetPackageDetail(int portalId, PackageInfo package)
        {
            var skinControl = SkinControlController.GetSkinControlByPackageID(package.PackageID);
            var detail = new SkinObjectPackageDetailDto(portalId, package);
            var isHostUser = UserController.Instance.GetCurrentUserInfo().IsSuperUser;

            detail.ControlKey = skinControl.ControlKey;
            detail.ControlSrc = skinControl.ControlSrc;
            detail.SupportsPartialRendering = skinControl.SupportsPartialRendering;
            detail.ReadOnly |= !isHostUser;
            
            return detail;
        }

        public bool SavePackageSettings(PackageSettingsDto packageSettings, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                string value;
                var skinControl = SkinControlController.GetSkinControlByPackageID(packageSettings.PackageId);

                if (packageSettings.EditorActions.TryGetValue("controlKey", out value)
                    && !string.IsNullOrEmpty(value))
                {
                    skinControl.ControlKey = value;
                }
                if (packageSettings.EditorActions.TryGetValue("controlSrc", out value)
                    && !string.IsNullOrEmpty(value))
                {
                    skinControl.ControlSrc = value;
                }
                if (packageSettings.EditorActions.TryGetValue("supportsPartialRendering", out value)
                    && !string.IsNullOrEmpty(value))
                {
                    bool b;
                    bool.TryParse(value, out b);
                    skinControl.SupportsPartialRendering = b;
                }

                SkinControlController.SaveSkinControl(skinControl);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                errorMessage = ex.Message;
                return false;
            }
        }

        #endregion
    }
}