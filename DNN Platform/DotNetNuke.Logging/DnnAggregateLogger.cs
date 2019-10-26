using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetNuke.Logging
{
    internal class DnnAggregateLogger : ILogger
    {
        private readonly ILogger[] _loggers;
        public DnnAggregateLogger(ILogger[] loggers)
        {
            // is this necessary? It prevents 1 api call?
            _loggers = loggers;
        }

        public DnnAggregateLogger(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers.ToArray();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            IDisposable[] disposables = new IDisposable[_loggers.Length];
            for (int index = 0; index < _loggers.Length; index++)
                disposables[index] = _loggers[index].BeginScope(state);

            return new AggregateDisposable(disposables);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            bool[] isLoggerEnabled = new bool[_loggers.Length];
            for (int index = 0; index < _loggers.Length; index++)
                isLoggerEnabled[index] = _loggers[index].IsEnabled(logLevel);

            return isLoggerEnabled.All(x => x);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            foreach (var logger in _loggers)
                logger.Log(logLevel, eventId, state, exception, formatter);
        }

        internal class AggregateDisposable : IDisposable
        {
            private readonly IDisposable[] _disposables;
            public AggregateDisposable(IEnumerable<IDisposable> disposables)
            {
                _disposables = disposables.ToArray();
            }

            // todo - Add proper IDisposable implementation
            public void Dispose()
            {
                for (int index = 0; index < _disposables.Length; index++)
                    _disposables[index].Dispose();
            }
        }
    }
}
