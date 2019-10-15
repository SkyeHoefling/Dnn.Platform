using DotNetNuke.AspNetCore.Mvc.RazorPages.StartupExtensions;
using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddModuleControl();
        }
    }
}
