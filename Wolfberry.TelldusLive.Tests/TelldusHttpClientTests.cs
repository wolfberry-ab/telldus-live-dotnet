using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Models;
using Xunit;

namespace Wolfberry.TelldusLive.Tests
{
    public class TelldusHttpClientTests
    {
        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _responseBody;
            private readonly HttpStatusCode _statusCode;

            public FakeHttpMessageHandler(string responseBody, HttpStatusCode statusCode = HttpStatusCode.OK)
            {
                _responseBody = responseBody;
                _statusCode = statusCode;
            }

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(_responseBody, Encoding.UTF8, "application/json")
                };
                return Task.FromResult(response);
            }
        }

        private static TelldusHttpClient CreateClient(string responseBody)
        {
            var fakeHandler = new FakeHttpMessageHandler(responseBody);
            var authenticator = Substitute.For<IAuthenticator>();
            authenticator.HttpClient.Returns(new HttpClient(fakeHandler));
            // InitializeHttpClient is called in the constructor, so we must set up HttpClient beforehand
            authenticator.When(a => a.InitializeHttpClient()).Do(_ => { });

            return new TelldusHttpClient(authenticator, "https://api.test.com");
        }

        [Fact]
        public async Task GetAsJsonAsync_ReturnsResponseString()
        {
            const string expectedJson = "{\"Status\":\"success\"}";
            var client = CreateClient(expectedJson);

            var result = await client.GetAsJsonAsync("https://api.test.com/json/device/list");

            Assert.Equal(expectedJson, result);
        }

        [Fact]
        public async Task GetResponseAsType_DeserializesResponse()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var json = JsonConvert.SerializeObject(mockedResponse);
            var client = CreateClient(json);

            var result = await client.GetResponseAsType<StatusResponse>("https://api.test.com/json/device/list");

            Assert.NotNull(result);
            Assert.Equal("success", result.Status);
        }

        [Fact]
        public void BaseUrl_ReturnsConfiguredBaseUrl()
        {
            var fakeHandler = new FakeHttpMessageHandler("{}");
            var authenticator = Substitute.For<IAuthenticator>();
            authenticator.HttpClient.Returns(new HttpClient(fakeHandler));
            authenticator.When(a => a.InitializeHttpClient()).Do(_ => { });

            var client = new TelldusHttpClient(authenticator, "https://custom.base.url");

            Assert.Equal("https://custom.base.url", client.BaseUrl);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            var client = CreateClient("{}");

            var exception = Record.Exception(() => client.Dispose());

            Assert.Null(exception);
        }
    }
}
