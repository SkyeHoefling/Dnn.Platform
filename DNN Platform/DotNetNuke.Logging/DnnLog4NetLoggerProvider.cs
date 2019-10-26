using log4net;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    internal class DnnLog4NetLoggerProvider : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName)
        {
            return new Log4NetLogger(LogManager.GetLogger(categoryName).Logger);
        }

        public void Dispose()
        {
            // left empty by design since the Log4Net logger doesn't have a dispose method.
        }
    }
}
