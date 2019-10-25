using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Util;
using System;
using System.Globalization;
using System.IO;

namespace DotNetNuke.Instrumentation
{
    public interface ILoggerThing
    {
        Microsoft.Extensions.Logging.ILogger GetLogger(Type type);
        Microsoft.Extensions.Logging.ILogger GetLogger(string name);
        Microsoft.Extensions.Logging.ILogger GetLogger();
    }
    public class LoggerThing : ILoggerThing
    {
        public Microsoft.Extensions.Logging.ILogger GetLogger(Type type)
        {
            return new LoggerSourceImpl.Logger(LogManager.GetLogger(type).Logger, type);
        }

        public Microsoft.Extensions.Logging.ILogger GetLogger(string name)
        {
            return new LoggerSourceImpl.Logger(LogManager.GetLogger(name).Logger, null);
        }

        public Microsoft.Extensions.Logging.ILogger GetLogger()
        {
            return new LoggerSourceImpl.Logger(LogManager.GetLogger("DotNetNuke").Logger, null);
        }
    }

    [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
    public class LoggerSourceImpl : ILoggerSource
    {
        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        public ILog GetLogger(Type type)
        {
            return new Logger(LogManager.GetLogger(type).Logger, type);
        }

        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        public ILog GetLogger(string name)
        {
            return new Logger(LogManager.GetLogger(name).Logger, null);
        }

        internal class Logger : LoggerWrapperImpl, ILog, Microsoft.Extensions.Logging.ILogger
        {
            private static Level _levelTrace;
            private static Level _levelDebug;
            private static Level _levelInfo;
            private static Level _levelWarn;
            private static Level _levelError;
            private static Level _levelFatal;
            //add custom logging levels (below trace value of 20000)
//            internal static Level LevelLogInfo = new Level(10001, "LogInfo");
//            internal static Level LevelLogError = new Level(10002, "LogError");

            private readonly Type _stackBoundary = typeof(DnnLogger);
            private const string ConfigFile = "DotNetNuke.log4net.config";
            private static bool _configured;
            private static readonly object ConfigLock = new object();

            internal Logger(ILogger logger, Type type) : base(logger)
            {
                _stackBoundary = type ?? typeof(Logger);
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
//                LevelLogError = levelMap.LookupWithDefault(LevelLogError);
//                LevelLogInfo = levelMap.LookupWithDefault(LevelLogInfo);

                //// Register custom logging levels with the default LoggerRepository
//                LogManager.GetRepository().LevelMap.Add(LevelLogInfo);
//                LogManager.GetRepository().LevelMap.Add(LevelLogError);

            }

            private static void AddGlobalContext()
            {
                try
                {
                    GlobalContext.Properties["appdomain"] = AppDomain.CurrentDomain.Id.ToString("D");
                    //bool isFullTrust = false;
                    //try
                    //{
                    //    CodeAccessPermission securityTest = new AspNetHostingPermission(AspNetHostingPermissionLevel.Unrestricted);
                    //    securityTest.Demand();
                    //    isFullTrust = true;
                    //}
                    //catch
                    //{
                    //    //code access security error
                    //    isFullTrust = false;
                    //}
                    //if (isFullTrust)
                    //{
                    //    GlobalContext.Properties["processid"] = Process.GetCurrentProcess().Id.ToString("D");
                    //}
                }
// ReSharper disable EmptyGeneralCatchClause
                catch
// ReSharper restore EmptyGeneralCatchClause
                {
                    //do nothing but just make sure no exception here.
                }
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsDebugEnabled { get { return Logger.IsEnabledFor(_levelDebug); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsInfoEnabled { get { return Logger.IsEnabledFor(_levelInfo); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsTraceEnabled { get { return Logger.IsEnabledFor(_levelTrace); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsWarnEnabled { get { return Logger.IsEnabledFor(_levelWarn); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsErrorEnabled { get { return Logger.IsEnabledFor(_levelError); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public bool IsFatalEnabled { get { return Logger.IsEnabledFor(_levelFatal); } }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Debug(object message)
            {
                Debug(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Debug(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelDebug, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void DebugFormat(string format, params object[] args)
            {
                DebugFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void DebugFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelDebug, new SystemStringFormat(provider, format, args), null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Info(object message)
            {
                Info(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Info(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelInfo, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void InfoFormat(string format, params object[] args)
            {
                InfoFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void InfoFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelInfo, new SystemStringFormat(provider, format, args), null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Trace(object message)
            {
                Trace(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Trace(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelTrace, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void TraceFormat(string format, params object[] args)
            {
                TraceFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void TraceFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelTrace, new SystemStringFormat(provider, format, args), null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Warn(object message)
            {
                Warn(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Warn(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelWarn, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void WarnFormat(string format, params object[] args)
            {
                WarnFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void WarnFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelWarn, new SystemStringFormat(provider, format, args), null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Error(object message)
            {
                Error(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Error(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelError, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void ErrorFormat(string format, params object[] args)
            {
                ErrorFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelError, new SystemStringFormat(provider, format, args), null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Fatal(object message)
            {
                Fatal(message, null);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void Fatal(object message, Exception exception)
            {
                Logger.Log(_stackBoundary, _levelFatal, message, exception);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void FatalFormat(string format, params object[] args)
            {
                FatalFormat(CultureInfo.InvariantCulture, format, args);
            }

            [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
            public void FatalFormat(IFormatProvider provider, string format, params object[] args)
            {
                Logger.Log(_stackBoundary, _levelFatal, new SystemStringFormat(provider, format, args), null);
            }

            void Microsoft.Extensions.Logging.ILogger.Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var message = formatter(state, exception);
                switch (logLevel)
                {
                    case Microsoft.Extensions.Logging.LogLevel.Trace:
                        Logger.Log(_stackBoundary, _levelTrace, message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Debug:
                        Logger.Log(_stackBoundary, _levelDebug, message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Information:
                        Logger.Log(_stackBoundary, _levelInfo, message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Warning:
                        Logger.Log(_stackBoundary, _levelWarn, message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Error:
                        Logger.Log(_stackBoundary, _levelError, message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Critical:
                        Logger.Log(_stackBoundary, _levelFatal, message, exception);
                        break;
                    default:
                        break;
                }
            }

            bool Microsoft.Extensions.Logging.ILogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case Microsoft.Extensions.Logging.LogLevel.Trace:
                        return IsTraceEnabled;
                    case Microsoft.Extensions.Logging.LogLevel.Debug:
                        return IsDebugEnabled;
                    case Microsoft.Extensions.Logging.LogLevel.Information:
                        return IsInfoEnabled;
                    case Microsoft.Extensions.Logging.LogLevel.Warning:
                        return IsWarnEnabled;
                    case Microsoft.Extensions.Logging.LogLevel.Error:
                        return IsErrorEnabled;
                    case Microsoft.Extensions.Logging.LogLevel.Critical:
                        return IsFatalEnabled;
                    default:
                        break;
                }

                return false;
            }

            IDisposable Microsoft.Extensions.Logging.ILogger.BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}