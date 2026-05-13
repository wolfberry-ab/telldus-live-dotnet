using System;
using Wolfberry.TelldusLive.Configuration;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Configuration
{
    public class ConfigurationExceptionTests
    {
        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var exception = new ConfigurationException();

            Assert.NotNull(exception);
        }

        [Fact]
        public void MessageConstructor_SetsMessage()
        {
            const string message = "Invalid configuration";

            var exception = new ConfigurationException(message);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void MessageAndInnerExceptionConstructor_SetsMessageAndInnerException()
        {
            const string message = "Outer error";
            var inner = new InvalidOperationException("inner");

            var exception = new ConfigurationException(message, inner);

            Assert.Equal(message, exception.Message);
            Assert.Same(inner, exception.InnerException);
        }
    }
}
