using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.UI.Modules;
using DotNetNuke.Web.Mvc.Framework.ActionResults;
using System.Web.Mvc;
using System.Web.UI;

namespace DotNetNuke.Web.Mvc
{
    public abstract class PageModel
    {
        public bool ValidateRequest { get; set; }
        public Page Page { get; set; }
        public ModuleInstanceContext ModuleContext { get; set; }
        public ModuleActionCollection ModuleActions { get; set; }
        public string LocalResourceFile { get; set; }
        public ViewEngineCollection ViewEngineCollectionEx { get; set; }
        public ActionResult ResultOfLastExecute
        {
            get
            {
                var result = new DnnViewResult
                {
                    ViewName = "Index",
                    ViewData = new ViewDataDictionary(this)
                };

                return result;
            }
        }
        public ControllerContext PageContext { get; set; }
    }
}
