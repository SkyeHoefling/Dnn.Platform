using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DotNetNuke.Logging
{
    public sealed class DnnLoggerFactory : ILoggerFactory
    {
        private readonly List<ILoggerProvider> _providers = new List<ILoggerProvider>();
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
            return new DnnLoggerFactory();
        }

        void ILoggerFactory.AddProvider(ILoggerProvider provider)
        {
            if (_providers.Contains(provider))
                return;

            _providers.Add(provider);
        }

        ILogger ILoggerFactory.CreateLogger(string categoryName)
        {
            ILogger[] loggers = new ILogger[_providers.Count];
            for (int index = 0; index < _providers.Count; index++)
                loggers[index] = _providers[index].CreateLogger(categoryName);

            return new DnnAggregateLogger(loggers);
        }

        // todo - Add proper IDisposable implementation
        void IDisposable.Dispose()
        {
            for (int index = 0; index < _providers.Count; index++)
                _providers[index].Dispose();

            _providers.Clear();
        }
    }
}
