using System;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class RepositoryExceptionTests
    {
        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var exception = new RepositoryException();

            Assert.NotNull(exception);
        }

        [Fact]
        public void MessageConstructor_SetsMessage()
        {
            const string message = "Repository error";

            var exception = new RepositoryException(message);

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void MessageAndInnerExceptionConstructor_SetsMessageAndInnerException()
        {
            const string message = "Outer repository error";
            var inner = new InvalidOperationException("inner");

            var exception = new RepositoryException(message, inner);

            Assert.Equal(message, exception.Message);
            Assert.Same(inner, exception.InnerException);
        }
    }
}
