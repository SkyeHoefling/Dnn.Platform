using Microsoft.Extensions.Logging;
using System;

namespace DotNetNuke.Logging
{
    public static class LoggerExtensions
    {
        public static void LogTrace(this ILogger logger, Exception exception)
        {
            logger.LogTrace(exception, exception.Message);
        }

        public static void LogInformation(this ILogger logger, Exception exception)
        {
            logger.LogInformation(exception, exception.Message);
        }

        public static void LogWarning(this ILogger logger, Exception exception)
        {
            logger.LogWarning(exception, exception.Message);
        }

        public static void LogDebug(this ILogger logger, Exception exception)
        {
            logger.LogDebug(exception, exception.Message);
        }

        public static void LogError(this ILogger logger, Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }

        public static void LogCritical(this ILogger logger, Exception exception)
        {
            logger.LogCritical(exception, exception.Message);
        }
    }
}
