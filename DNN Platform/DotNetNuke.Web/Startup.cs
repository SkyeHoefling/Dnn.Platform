using DotNetNuke.DependencyInjection;
using DotNetNuke.DependencyInjection.Extensions;
using DotNetNuke.Logging;
using DotNetNuke.Web.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace DotNetNuke.Web
{
    public class Startup : IDnnStartup
    {
        private readonly ILogger _logger;
        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
            Configure();
        }

        private void Configure()
        {
            var services = new DependencyInjection.Startup().Services;
            ConfigureServices(services);
            DependencyProvider = services.BuildServiceProvider();
        }

        public IServiceProvider DependencyProvider { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var startupTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x != Assembly.GetAssembly(typeof(Startup)))
                .SelectMany(x => x.SafeGetTypes())
                .Where(x => typeof(IDnnStartup).IsAssignableFrom(x) &&
                            x.IsClass &&
                            !x.IsAbstract);

            var startupInstances = startupTypes
                .Select(x => CreateInstance(x))
                .Where(x => x != null);

            foreach (IDnnStartup startup in startupInstances)
            {
                try
                {
                    startup.ConfigureServices(services);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Unable to configure services for {typeof(Startup).FullName}, see exception for details", ex);
                }
            }

            services.AddWebApi();
        }

        private object CreateInstance(Type startupType)
        {
            IDnnStartup startup = null;
            try
            {
                startup = (IDnnStartup)Activator.CreateInstance(startupType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to instantiate startup code for {startupType.FullName}", ex);
            }

            return startup;
        }
    }
}
