using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class GroupRepositoryTests
    {
        private const string MockedUrl = "https://mocked.url";

        private static ITelldusHttpClient CreateMockClient(string responseJson)
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(default).ReturnsForAnyArgs(responseJson);
            return client;
        }

        [Fact]
        public async Task AddGroupAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IGroupRepository repository = new GroupRepository(client);

            var result = await repository.AddGroupAsync("1", "Living Room", "10,20");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveGroupAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IGroupRepository repository = new GroupRepository(client);

            var result = await repository.RemoveGroupAsync("5");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task AddGroupAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Access denied" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IGroupRepository repository = new GroupRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.AddGroupAsync("1", "Test", null));
        }

        [Fact]
        public async Task RemoveGroupAsync_NullResponse_ThrowsException()
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            IGroupRepository repository = new GroupRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.RemoveGroupAsync("5"));
        }
    }
}
