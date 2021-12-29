using Microsoft.Extensions.Logging;
using LoggerDisposableExtensions;

namespace LoggerDisposableExtensionsTest.TestHelpers
{
    public class CalculatorService
    {
        private readonly ILogger<CalculatorService> _logger;

        public CalculatorService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        public int Add(int a, int b, LogLevel logLevel)
        {
            using var _ = _logger.TimedOperation(logLevel, "Add {0} and {1}", a, b);
            return a + b;
        }
    }
}
