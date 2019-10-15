using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages.StartupExtensions
{
    internal static class StartupExtensions_AddModuleControl
    {
        internal static void AddModuleControl(this IServiceCollection services)
        {
            services.AddSingleton<RazorPagesModuleControlFactory>();
            services.AddTransient<IHostControlBuilder, RazorPagesHostControlBuilder>();
            services.AddTransient<IHostControl, RazorPagesHostControl>();
            services.AddTransient<IEngine, RazorPagesEngine>();
        }
    }
}
