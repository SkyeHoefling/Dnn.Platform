using DotNetNuke.Entities.Modules;

namespace Dnn.EditBar.UI.Components
{
    public class BusinessController : IUpgradeable
    {
        public string UpgradeModule(string version)
        {
            switch (version)
            {
                case "01.00.00":
                    break;
            }

            return "Success";
        }
    }
}
