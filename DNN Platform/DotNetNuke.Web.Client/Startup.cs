using DotNetNuke.Abstractions.Clients.ClientResourceManagement;
using DotNetNuke.DependencyInjection;
using DotNetNuke.Web.Client.ClientResourceManagement;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.Web.Client
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientResourceManager, ClientResourceManager>();
        }
    }
}
