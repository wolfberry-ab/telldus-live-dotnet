using Wolfberry.TelldusLive.Utils;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Utils
{
    public class UrlBuilderTests
    {
        private const string BaseUrl = "https://wolfberry.se:443/api";

        [Fact]
        public void BuildSimpleUrl_ReturnsValidString()
        {
            var builder = new UrlBuilder(BaseUrl);
            var url = builder.Build();

            Assert.Equal(BaseUrl, url);
        }

        [Fact]
        public void BuildComplexUrl_ReturnsValidString()
        {
            var urlWithoutParameters = $"{BaseUrl}/json/event/setAction";
            var urlBuilder = new UrlBuilder(urlWithoutParameters);
            const string id = "123";
            const string message = "New alarm!";
            urlBuilder.AddQuery("id", id);
            urlBuilder.AddAsEscapedQuery("message", message);
            urlBuilder.AddQuery("delay", 1);
            urlBuilder.AddQuery("method", "push");
            urlBuilder.AddQuery("playSound", true);

            var url = urlBuilder.Build();

            var expectedUrl = $"{urlWithoutParameters}?id={id}&message=New%20alarm%21";
            expectedUrl += "&delay=1&method=push&playSound=1";
            Assert.Equal(expectedUrl, url);
        }

        [Fact]
        public void AddOptionalQuery_NullBool_DoesNotAddQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalQuery("flag", (bool?)null);
            var url = urlBuilder.Build();

            Assert.Equal(BaseUrl, url);
        }

        [Fact]
        public void AddOptionalQuery_NonNullBool_AddsQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalQuery("flag", (bool?)true);
            var url = urlBuilder.Build();

            Assert.Contains("flag=1", url);
        }

        [Fact]
        public void AddOptionalQuery_NullInt_DoesNotAddQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalQuery("count", (int?)null);
            var url = urlBuilder.Build();

            Assert.Equal(BaseUrl, url);
        }

        [Fact]
        public void AddOptionalQuery_NonNullInt_AddsQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalQuery("count", (int?)5);
            var url = urlBuilder.Build();

            Assert.Contains("count=5", url);
        }

        [Fact]
        public void AddOptionalEscapedQuery_NonNullValue_AddsEscapedQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalEscapedQuery("name", "hello world");
            var url = urlBuilder.Build();

            Assert.Contains("name=hello%20world", url);
        }

        [Fact]
        public void AddOptionalEscapedQuery_NullValue_DoesNotAddQuery()
        {
            var urlBuilder = new UrlBuilder(BaseUrl);
            urlBuilder.AddOptionalEscapedQuery("name", null);
            var url = urlBuilder.Build();

            Assert.Equal(BaseUrl, url);
        }
    }
}
