using Microsoft.Extensions.Logging;
using System;

namespace LoggerDisposableExtensions
{
    public static class LoggerExtensions
    {
        public static IDisposable TimedOperation<T>(this ILogger<T> logger, LogLevel logLevel, string message, params object?[] args)
        {
            return new TimedLogOperation<T>(logger, logLevel, message, args);
        }
    }
}
