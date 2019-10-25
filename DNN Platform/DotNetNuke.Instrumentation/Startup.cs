using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Instrumentation
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILogger>(x => new LoggerThing().GetLogger());
            services.AddTransient<ILoggerThing, LoggerThing>();
        }
    }
}
