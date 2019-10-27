using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection
{
    public class Startup
    {
        public IServiceCollection Services { get; }
        public Startup()
        {
            Services = new DnnServiceCollection();
        }
    }
}
