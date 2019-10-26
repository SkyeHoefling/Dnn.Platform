using System;

namespace DotNetNuke.Instrumentation
{
    [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v12.0.0.")]
    public static class LoggerSource 
    {
        static ILoggerSource _instance = new LoggerSourceImpl();

        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v12.0.0.")]
        public static ILoggerSource Instance
        {
            get { return _instance; }
        }

        [Obsolete("Deprecated in Platform 9.4.2. Scheduled removal in v12.0.0.")]
        public static void SetTestableInstance(ILoggerSource loggerSource)
        {
            _instance = loggerSource;
        }
    }
}