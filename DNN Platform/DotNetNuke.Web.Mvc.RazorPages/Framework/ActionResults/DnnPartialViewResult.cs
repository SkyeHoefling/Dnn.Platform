using DotNetNuke.Common;
using DotNetNuke.Web.Mvc.RazorPages.SDK.NETFramework;
using System;
using System.IO;
using System.Web.Mvc;

namespace DotNetNuke.Web.Mvc.RazorPages.Framework.ActionResults
{
    public class DnnPartialViewResult : PartialViewResult, IDnnViewResult
    {
        public void ExecuteResult(ControllerContext context, TextWriter writer)
        {
            Requires.NotNull("context", context);
            Requires.NotNull("writer", writer);

            if (String.IsNullOrEmpty(ViewName))
            {
                ViewName = context.RouteData.GetRequiredString("action");
            }

            ViewEngineResult result = null;

            if (View == null)
            {
                result = ViewEngineCollection.FindPartialView(context, ViewName);
                View = result.View;
            }

            var viewContext = new ViewContext(context, View, ViewData, TempData, writer);
            View.Render(viewContext, writer);

            if (result != null)
            {
                result.ViewEngine.ReleaseView(context, View);
            }
        }
    }
}