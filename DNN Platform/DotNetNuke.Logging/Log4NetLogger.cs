using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using ILog4NetLogger = log4net.Core.ILogger;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DotNetNuke.Logging
{
    internal class Log4NetLogger : LoggerWrapperImpl, ILogger
    {
        private static Level _levelTrace;
        private static Level _levelDebug;
        private static Level _levelInfo;
        private static Level _levelWarn;
        private static Level _levelError;
        private static Level _levelFatal;

        private readonly Type _stackBoundary;
        private const string ConfigFile = "DotNetNuke.log4net.config";
        private static bool _configured;
        private static readonly object ConfigLock = new object();

        internal Log4NetLogger() : this(LogManager.GetLogger("DotNetNuke").Logger) { }
        internal Log4NetLogger(ILog4NetLogger logger) : this(logger, null) { }
        internal Log4NetLogger(ILog4NetLogger logger, Type type) : base(logger)
        {
            _stackBoundary = type ?? typeof(Log4NetLogger);
            EnsureConfig();
            ReloadLevels(logger.Repository);
        }

        private static void EnsureConfig()
        {
            if (!_configured)
            {
                lock (ConfigLock)
                {
                    if (!_configured)
                    {
                        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile);
                        var originalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ConfigFile);
                        if (!File.Exists(configPath) && File.Exists(originalPath))
                        {
                            File.Copy(originalPath, configPath);
                        }

                        if (File.Exists(configPath))
                        {
                            AddGlobalContext();
                            XmlConfigurator.ConfigureAndWatch(new FileInfo(configPath));
                        }
                        _configured = true;
                    }

                }
            }
        }

        private static void ReloadLevels(ILoggerRepository repository)
        {
            LevelMap levelMap = repository.LevelMap;

            _levelTrace = levelMap.LookupWithDefault(Level.Trace);
            _levelDebug = levelMap.LookupWithDefault(Level.Debug);
            _levelInfo = levelMap.LookupWithDefault(Level.Info);
            _levelWarn = levelMap.LookupWithDefault(Level.Warn);
            _levelError = levelMap.LookupWithDefault(Level.Error);
            _levelFatal = levelMap.LookupWithDefault(Level.Fatal);
        }

        private static void AddGlobalContext()
        {
            try
            {
                GlobalContext.Properties["appdomain"] = AppDomain.CurrentDomain.Id.ToString("D");
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                //do nothing but just make sure no exception here.
            }
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // NOTE - AH:
            // The exception isn't being logged correctly.
            // Ex: _logger.LogError(exc);
            // Output: 2019-10-27 22:41:11.009-04:00 [DESKTOP-8L05G7S][D:3][T:1][ERROR] DotNetNuke.ComponentModel.ProviderInstaller - 
            // 
            // See there is no message provided with the log statement.
            //
            // Potential Solution: The actual details of the exception are not being formatted correctly and the message
            //                     is returning an empty string. Maybe we need to trace the formatter up the stack and leverage
            //                     the object->string mapper that is in the log4net project.

            var message = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    Logger.Log(_stackBoundary, _levelTrace, message, exception);
                    break;
                case LogLevel.Debug:
                    Logger.Log(_stackBoundary, _levelDebug, message, exception);
                    break;
                case LogLevel.Information:
                    Logger.Log(_stackBoundary, _levelInfo, message, exception);
                    break;
                case LogLevel.Warning:
                    Logger.Log(_stackBoundary, _levelWarn, message, exception);
                    break;
                case LogLevel.Error:
                    Logger.Log(_stackBoundary, _levelError, message, exception);
                    break;
                case LogLevel.Critical:
                    Logger.Log(_stackBoundary, _levelFatal, message, exception);
                    break;
                default:
                    break;
            }
        }

        bool ILogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return Logger.IsEnabledFor(_levelTrace);
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return Logger.IsEnabledFor(_levelDebug);
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return Logger.IsEnabledFor(_levelInfo);
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return Logger.IsEnabledFor(_levelWarn);
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return Logger.IsEnabledFor(_levelError);
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return Logger.IsEnabledFor(_levelFatal);
                default:
                    break;
            }

            return false;
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
