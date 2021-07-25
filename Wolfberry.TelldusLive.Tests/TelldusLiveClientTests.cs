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
    }
}
