using DotNetNuke.DependencyInjection;
using log4net;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    [LockedService]
    internal class DnnLog4NetLoggerProvider : ILoggerProvider
    {
        public readonly string _configFile;
        internal DnnLog4NetLoggerProvider()
        {
            _configFile = "DotNetNuke.log4net.config";
        }

        internal DnnLog4NetLoggerProvider(string configFile)
        {
            _configFile = configFile;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Log4NetLogger(LogManager.GetLogger(categoryName).Logger);
        }

        public void Dispose()
        {
            // todo - ah: research the recommendations from MSFT
            // left empty by design since the Log4Net logger doesn't have a dispose method.
        }
    }
}
