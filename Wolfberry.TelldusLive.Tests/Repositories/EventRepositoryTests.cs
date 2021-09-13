using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Repositories;
using Wolfberry.TelldusLive.Utils;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class EventRepositoryTests
    {
        private const string MockedUrl = "https://mocked.url";

        [Fact]
        public async Task RemoveActionAsync_ReturnsOk()
        {
            var mockedResponse = new StatusResponse
            {
                Status = "success"
            };
            var mockedResponseJson = JsonUtil.Serialize(mockedResponse);
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl
                .Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(MockedUrl)
                .ReturnsForAnyArgs(mockedResponseJson);
            var repository = new EventRepository(telldusClient);
            const string actionId = "123";

            var status = await repository.RemoveActionAsync(actionId);

            Assert.Equal(mockedResponse.Status, status.Status);
        }

        [Fact]
        public async Task RemoveActionAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse
            {
                Error = "Action \"123\" not found!"
            };
            var mockedResponseJson = JsonUtil.Serialize(mockedResponse);
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl
                .Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(MockedUrl)
                .ReturnsForAnyArgs(mockedResponseJson);
            var repository = new EventRepository(telldusClient);
            const string actionId = "123";

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.RemoveActionAsync(actionId));
        }

        [Fact]
        public async Task RemoveActionAsync_EmptyResponse_ThrowsException()
        {
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl
                .Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(Arg.Any<string>())
                .ReturnsNull();
            var repository = new EventRepository(telldusClient);
            const string actionId = "123";

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.RemoveActionAsync(actionId));
        }
    }
}
