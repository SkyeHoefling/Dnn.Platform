using System;

namespace DotNetNuke.Instrumentation
{
    [Obsolete("Use \"ILogger GetLogger()\" instead. Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
    public interface ILoggerSource
    {
        [Obsolete("Use \"ILogger GetLogger()\" instead. Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        ILog GetLogger(Type type);

        [Obsolete("Use \"ILogger GetLogger()\" instead. Deprecated in Platform 9.4.2. Scheduled removal in v11.0.0.")]
        ILog GetLogger(string name);
    }
}