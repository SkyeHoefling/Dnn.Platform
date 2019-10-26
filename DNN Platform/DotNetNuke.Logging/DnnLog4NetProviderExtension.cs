using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    internal static class DnnLog4NetProviderExtension
    {
        internal static ILoggerFactory AddDnnLog4Net(this ILoggerFactory factory)
        {
            factory.AddProvider(new DnnLog4NetLoggerProvider());
            return factory;
        }
    }
}
