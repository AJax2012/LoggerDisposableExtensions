using LoggerDisposableExtensionsTest.TestHelpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace LoggerDisposableExtensionsTest
{
    [TestFixture]
    public class LoggerExtensionsTest
    {
        private Mock<ILogger<CalculatorService>> _logger;
        private CalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<CalculatorService>>();
            _calculatorService = new CalculatorService(_logger.Object);
        }

        [TestCase(LogLevel.Information)]
        [TestCase(LogLevel.Trace)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Critical)]
        [TestCase(LogLevel.None)]
        public void TimedOperation_Should_Log_Correct_LogLevel(LogLevel logLevel)
        {
            _calculatorService.Add(1, 2, logLevel);
            _logger.Verify(x => x.Log(
                    It.Is<LogLevel>(y => y == logLevel),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public void TimedOperation_Should_Log_Correct_Message()
        {
            var expectedMessage = @"Add 1 and 2 completed in \d*ms";
            var actualMessage = string.Empty;

            _logger.Setup(x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Delegate>(
                    (level, eventid, state, ex, func) =>
                    {
                        actualMessage = state.ToString();
                    }
                );

            _calculatorService.Add(1, 2, LogLevel.Information);

            Assert.That(actualMessage, Does.Match(expectedMessage));
        }
    }
}