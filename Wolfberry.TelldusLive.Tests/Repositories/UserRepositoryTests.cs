using System.Collections.Generic;
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

        [Fact]
        public async Task GetSmsHistoryAsync_ReturnsHistory()
        {
            var mockedHistory = new List<SmsHistoryEntry>
            {
                new SmsHistoryEntry { Id = "sms1", To = "46709000001" }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedHistory));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.GetSmsHistoryAsync();

            Assert.NotNull(result);
            Assert.Single(result.History);
        }

        [Fact]
        public async Task ActivateCouponAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.ActivateCouponAsync("COUPON123");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task ChangeLocaleAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.ChangeLocaleAsync("sv_SE");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.ChangePasswordAsync("oldpass", "newpass");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task DeletePushTokenAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.DeletePushTokenAsync("token-abc");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetEulaAsync_ReturnsEula()
        {
            var mockedResponse = new EulaResponse { Version = 3, Text = "Terms" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.GetEulaAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Version);
        }

        [Fact]
        public async Task AddLinkedAccountAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.AddLinkedAccountAsync("google", "id-token-123");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetLinkedAccountsAsync_ReturnsResult()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.GetLinkedAccountsAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task RemoveLinkedAccountAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.RemoveLinkedAccountAsync("google");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetPhonesAsync_ReturnsPhones()
        {
            var mockedResponse = new PhonesResponse
            {
                Phone = new List<Phone> { new Phone { Id = "ph1", Name = "iPhone" } }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.GetPhonesAsync();

            Assert.NotNull(result);
            Assert.Single(result.Phone);
        }

        [Fact]
        public async Task RegisterPushTokenAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.RegisterPushTokenAsync(
                "token", "My Phone", "iPhone 15", "Apple", "17.0", "device-id", "apns");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SendPushTestAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.SendPushTestAsync("ph1", "Test message");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task UnregisterPushToken_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IUserRepository repository = new UserRepository(client);

            var result = await repository.UnregisterPushToken("token-abc");

            Assert.Equal("success", result.Status);
        }
    }
}
