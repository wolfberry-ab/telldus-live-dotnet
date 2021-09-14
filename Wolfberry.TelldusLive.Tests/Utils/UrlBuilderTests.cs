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
    }
}
