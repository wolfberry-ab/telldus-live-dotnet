using System;
using Wolfberry.TelldusLive.Configuration;
using Xunit;

namespace Wolfberry.TelldusLive.Tests
{
    public class TelldusLiveClientTests
    {
        [Fact]
        public void InitializationTest()
        {
            const string consumerKey = "123";
            const string consumerKeySecret = "456";
            const string accessToken = "789";
            const string accessTokenSecret = "012";

            ITelldusLiveClient client = new TelldusLiveClient(
                consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

            Assert.NotNull(client.Sensors);
            Assert.NotNull(client.Clients);
            Assert.NotNull(client.Devices);
            Assert.NotNull(client.Events);
            Assert.NotNull(client.Groups);
            Assert.NotNull(client.Scheduler);
            Assert.NotNull(client.User);
        }

        [Fact]
        public void ConfigureWithCustomBaseUrl_ReturnsValidClient()
        {
            ITelldusLiveClient client = new TelldusLiveClient(
                "key", "secret", "token", "tokensecret",
                customBaseUrl: "https://custom.api.example.com");

            Assert.NotNull(client.Sensors);
            Assert.NotNull(client.Devices);
        }

        [Fact]
        public void Initialize_MissingConsumerKey_ThrowsConfigurationException()
        {
            Assert.Throws<ConfigurationException>(() =>
                new TelldusLiveClient(null, "secret", "token", "tokensecret"));
        }

        [Fact]
        public void Initialize_MissingConsumerKeySecret_ThrowsConfigurationException()
        {
            Assert.Throws<ConfigurationException>(() =>
                new TelldusLiveClient("key", null, "token", "tokensecret"));
        }

        [Fact]
        public void Initialize_MissingAccessToken_ThrowsConfigurationException()
        {
            Assert.Throws<ConfigurationException>(() =>
                new TelldusLiveClient("key", "secret", null, "tokensecret"));
        }

        [Fact]
        public void Initialize_MissingAccessTokenSecret_ThrowsConfigurationException()
        {
            Assert.Throws<ConfigurationException>(() =>
                new TelldusLiveClient("key", "secret", "token", null));
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            var client = new TelldusLiveClient("key", "secret", "token", "tokensecret");

            var exception = Record.Exception(() => client.Dispose());

            Assert.Null(exception);
        }
    }
}
