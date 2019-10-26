using DotNetNuke.Logging;
using log4net.ObjectRenderer;
using log4net.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DotNetNuke.Instrumentation
{
    [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
    public class LoggerSourceImpl : ILoggerSource
    {
        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        public ILog GetLogger(Type type)
        {
            return new Logger(DnnLoggerFactory.Instance.CreateLogger(type.FullName));
        }

        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        public ILog GetLogger(string name)
        {
            return new Logger(DnnLoggerFactory.Instance.CreateLogger(name));
        }

        internal class Logger : ILog
        {
            private readonly ILogger _logger;
            internal Logger(ILogger logger)
            {
                _logger = logger;
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsDebugEnabled { get { return _logger.IsEnabled(LogLevel.Debug); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsInfoEnabled { get { return _logger.IsEnabled(LogLevel.Information); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsTraceEnabled { get { return _logger.IsEnabled(LogLevel.Trace); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsWarnEnabled { get { return _logger.IsEnabled(LogLevel.Warning); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsErrorEnabled { get { return _logger.IsEnabled(LogLevel.Error); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsFatalEnabled { get { return _logger.IsEnabled(LogLevel.Critical); } }

            private string MapToString(object message)
            {
                var mapper = new RendererMap();
                return mapper.FindAndRender(message);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Debug(object message)
            {
                Debug(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Debug(object message, Exception exception)
            {
                _logger.LogDebug(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void DebugFormat(string format, params object[] args)
            {
                DebugFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void DebugFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogDebug(messageFormatter.ToString());
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Info(object message)
            {
                Info(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Info(object message, Exception exception)
            {
                _logger.LogInformation(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void InfoFormat(string format, params object[] args)
            {
                InfoFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void InfoFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogInformation(messageFormatter.ToString());
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Trace(object message)
            {
                Trace(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Trace(object message, Exception exception)
            {
                _logger.LogTrace(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void TraceFormat(string format, params object[] args)
            {
                TraceFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void TraceFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogTrace(messageFormatter.ToString());
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Warn(object message)
            {
                Warn(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Warn(object message, Exception exception)
            {
                _logger.LogWarning(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void WarnFormat(string format, params object[] args)
            {
                WarnFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void WarnFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogWarning(messageFormatter.ToString());
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Error(object message)
            {
                Error(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Error(object message, Exception exception)
            {
                _logger.LogError(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void ErrorFormat(string format, params object[] args)
            {
                ErrorFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogError(messageFormatter.ToString());
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Fatal(object message)
            {
                Fatal(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Fatal(object message, Exception exception)
            {
                _logger.LogCritical(MapToString(message), exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void FatalFormat(string format, params object[] args)
            {
                FatalFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void FatalFormat(IFormatProvider provider, string format, params object[] args)
            {
                var messageFormatter = new SystemStringFormat(provider, format, args);
                _logger.LogCritical(messageFormatter.ToString());
            }
        }
    }
}