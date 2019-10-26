using log4net;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetNuke.Logging
{
    public static class DnnLogger
    {
        private static readonly Lazy<ILogger> _logger = new Lazy<ILogger>(() => CreateLogger());

        internal static ILogger CreateLogger()
        {
            return new Log4NetLogger(LogManager.GetLogger("DotNetNuke").Logger);
        }
    }
}
