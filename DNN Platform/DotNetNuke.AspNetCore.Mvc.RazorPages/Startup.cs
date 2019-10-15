using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RazorPagesModuleControlFactory>();
            services.AddTransient<IHostControlBuilder, RazorPagesHostControlBuilder>();
            services.AddTransient<IHostControl, RazorPagesHostControl>();
            services.AddTransient<IEngine, RazorPagesEngine>();
        }
    }
}
