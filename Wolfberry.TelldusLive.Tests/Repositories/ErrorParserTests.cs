using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class ErrorParserTests
    {
        [Fact]
        public void GetOrCreateErrorMessage_NullInput_ReturnsErrorMessage()
        {
            var result = ErrorParser.GetOrCreateErrorMessage(null);

            Assert.NotNull(result);
            Assert.Contains("Null or empty json string", result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_EmptyString_ReturnsErrorMessage()
        {
            var result = ErrorParser.GetOrCreateErrorMessage("");

            Assert.NotNull(result);
            Assert.Contains("Null or empty json string", result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_DnsError_ReturnsErrorMessage()
        {
            var result = ErrorParser.GetOrCreateErrorMessage("error code: 1001");

            Assert.NotNull(result);
            Assert.Contains("error code: 1001", result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_ErrorStringCaseInsensitive_ReturnsErrorMessage()
        {
            var result = ErrorParser.GetOrCreateErrorMessage("Error something went wrong");

            Assert.NotNull(result);
            Assert.Contains("Error something went wrong", result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_JsonWithErrorField_ReturnsErrorMessage()
        {
            const string json = "{\"error\": \"Device not found\"}";

            var result = ErrorParser.GetOrCreateErrorMessage(json);

            Assert.NotNull(result);
            Assert.Contains("Device not found", result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_ValidJsonNoError_ReturnsNull()
        {
            const string json = "{\"status\": \"success\"}";

            var result = ErrorParser.GetOrCreateErrorMessage(json);

            Assert.Null(result);
        }

        [Fact]
        public void GetOrCreateErrorMessage_JsonArray_ReturnsNull()
        {
            const string json = "[{\"id\": \"1\"}, {\"id\": \"2\"}]";

            var result = ErrorParser.GetOrCreateErrorMessage(json);

            Assert.Null(result);
        }
    }
}
