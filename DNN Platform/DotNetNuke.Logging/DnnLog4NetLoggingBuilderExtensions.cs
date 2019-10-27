using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    internal static class DnnLog4NetLoggingBuilderExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.AddProvider(new DnnLog4NetLoggerProvider());
            return builder;
        }
    }
}
