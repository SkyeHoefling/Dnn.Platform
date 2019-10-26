using log4net;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetNuke.Logging
{
    internal class DnnLoggerFactory : IDnnLoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Log4NetLogger(LogManager.GetLogger(categoryName).Logger);
        }

        public ILogger CreateLogger(Type type)
        {
            return CreateLogger(type.FullName);
        }

        public void Dispose()
        {
        }
    }
}
