using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace LoggerDisposableExtensions
{
    public class TimedLogOperation<T> : IDisposable
    {
        private readonly ILogger<T> _logger;
        private readonly LogLevel _logLevel;
        private readonly string _message;
        private readonly object?[] _args;
        private readonly Stopwatch _stopwatch;

        public TimedLogOperation(ILogger<T> logger, LogLevel logLevel, string message, object?[] args = null)
        {
            _logger = logger;
            _logLevel = logLevel;
            _message = message;
            _args = args;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.Log(
                _logLevel, 
                $"{_message} completed in {_stopwatch.ElapsedMilliseconds}ms", 
                _args);
        }
    }
}
