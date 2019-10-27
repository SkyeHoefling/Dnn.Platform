using Microsoft.Extensions.Logging;
using System;

namespace DotNetNuke.Logging
{
    public sealed class DnnLoggerFactory : LoggerFactory
    {
        private static readonly Lazy<ILoggerFactory> _lazyInstance = new Lazy<ILoggerFactory>(() => Builder());
        private DnnLoggerFactory() { }

        // This is marked deprecated as the factory pattern should not be used going forward.
        // Dependency Injection should be used if possible.
        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v12.0.0.")]
        public static ILoggerFactory Instance
        {
            get => _lazyInstance.Value;
        }

        internal static ILoggerFactory Builder()
        {
            var factory = new LoggerFactory();
            factory.AddProvider(new DnnLog4NetLoggerProvider());
            return factory;
            // TODO - Resolve the singleton instance of ILoggerFactory that is used
            // in the dependency injection container
            //throw new NotImplementedException("Unable to resolve ILoggerFactory");
        }
    }
}
