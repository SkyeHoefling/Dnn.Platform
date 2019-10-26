using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DotNetNuke.Logging
{
    internal class DnnLoggerFactory : ILoggerFactory
    {
        private readonly List<ILoggerProvider> _providers = new List<ILoggerProvider>();
        public void AddProvider(ILoggerProvider provider)
        {
            if (_providers.Contains(provider))
                return;

            _providers.Add(provider);
        }

        public ILogger CreateLogger(string categoryName)
        {
            ILogger[] loggers = new ILogger[_providers.Count];
            for (int index = 0; index < _providers.Count; index++)
                loggers[index] = _providers[index].CreateLogger(categoryName);

            return new DnnAggregateLogger(loggers);
        }

        // todo - Add proper IDisposable implementation
        public void Dispose()
        {
            for (int index = 0; index < _providers.Count; index++)
                _providers[index].Dispose();

            _providers.Clear();
        }
    }
}
