using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.User;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class UserRepositoryTests
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
        public async Task GetProfileAsync_ReturnsProfile()
        {
            var mockedResponse = new ProfileResponse
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john@example.com"
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.GetProfileAsync();

            Assert.Equal("John", result.Firstname);
            Assert.Equal("Doe", result.Lastname);
            Assert.Equal("john@example.com", result.Email);
        }

        [Fact]
        public async Task AcceptEulaAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.AcceptEulaAsync(1);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task ChangeEmailAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.ChangeEmailAsync("new@example.com");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetNameAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.SetNameAsync("Jane", "Doe");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetProfileAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Unauthorized" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetProfileAsync());
        }

        [Fact]
        public async Task GetProfileAsync_NullResponse_ThrowsException()
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            IUserRepository repository = new UserRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetProfileAsync());
        }
    }
}
