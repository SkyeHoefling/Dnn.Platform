using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(x => CreateLoggerFactory());
            services.AddTransient(x => x.GetService<ILoggerFactory>().CreateLogger("DotNetNuke"));

            ILoggerFactory CreateLoggerFactory() =>  
                DnnLoggerFactory.Builder()
                    .AddDnnLog4Net();
        }
    }
}
