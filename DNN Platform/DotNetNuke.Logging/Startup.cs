using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDnnLoggerFactory, DnnLoggerFactory>();
            services.AddTransient<ILoggerFactory, DnnLoggerFactory>();
            services.AddTransient<ILogger, Log4NetLogger>();
        }
    }
}
