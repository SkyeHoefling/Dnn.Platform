using Microsoft.Extensions.Logging;
using System;

namespace DotNetNuke.Logging
{
    public interface IDnnLoggerFactory : ILoggerFactory
    {
        ILogger CreateLogger(Type type);
    }
}
