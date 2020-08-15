namespace DotNetNuke.Web.Client
{
    using DotNetNuke.Abstractions.Clients.ClientResourceManagement;
    using DotNetNuke.DependencyInjection;
    using DotNetNuke.Web.Client.ClientResourceManagement;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientDependencySettings, ClientDependencySettings>();
            services.AddTransient<IClientResourceManager, ClientResourceManager>();
        }
    }
}
