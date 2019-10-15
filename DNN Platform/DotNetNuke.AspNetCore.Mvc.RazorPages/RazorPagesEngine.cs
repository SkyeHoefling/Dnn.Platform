using DotNetNuke.UI.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    internal class RazorPagesEngine : IEngine
    {
        protected IRazorViewEngine ViewEngine { get; }
        protected ITempDataProvider TempDataProvider { get; }
        protected IServiceProvider ServiceProvider { get; }

        public RazorPagesEngine(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            ViewEngine = viewEngine;
            TempDataProvider = tempDataProvider;
            ServiceProvider = serviceProvider;
        }

        public void Render(string file, ModuleInstanceContext moduleContext, string localResourceFile, TextWriter writer)
        {
            var actionContext = GetActionContext();
            var viewEngineResult = ViewEngine.FindView(GetActionContext(), file, false);

            if (!viewEngineResult.Success)
                throw new InvalidOperationException($"Couldn't find view '{file}'");

            var view = viewEngineResult.View;
            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary()),
                    new TempDataDictionary(actionContext.HttpContext, TempDataProvider),
                    output,
                    new HtmlHelperOptions());

                view.RenderAsync(viewContext).RunSynchronously();
            }
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = ServiceProvider
            };

            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
