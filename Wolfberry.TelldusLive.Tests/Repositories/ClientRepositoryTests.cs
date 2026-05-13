using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Client;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class ClientRepositoryTests
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
        public async Task GetClientsAsync_NoArguments_ReturnsOk()
        {
            const string expectedId = "123";
            var mockedClients = new ClientsResponse
            {
                Client = new List<Client>
                {
                    new Client
                    {
                        Id = expectedId
                    }
                }
            };
            var telldusClient = CreateMockClient(JsonConvert.SerializeObject(mockedClients));

            IClientRepository repository = new ClientRepository(telldusClient);

            var clients = await repository.GetClientsAsync();

            Assert.NotNull(clients);
            Assert.Equal(expectedId, clients.Client.First().Id);
        }

        [Fact]
        public async Task GetClientInfoAsync_ReturnsClientInfo()
        {
            var mockedResponse = new ClientInfoResponse { Id = "c1", Name = "TellStick" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.GetClientInfoAsync("c1");

            Assert.NotNull(result);
            Assert.Equal("c1", result.Id);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.RegisterAsync("c1", "uuid-001");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.RemoveAsync("c1");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetCoordinatesAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.SetCoordinatesAsync("c1", 10.5, 20.5);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetNameAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.SetNameAsync("c1", "New Name");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task EnablePushAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.EnablePushAsync("c1", true);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetTimezoneAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.SetTimezoneAsync("c1", "Europe/Stockholm");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task TransferAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IClientRepository repository = new ClientRepository(client);

            var result = await repository.TransferAsync("c1", "new@example.com");

            Assert.Equal("success", result.Status);
        }
    }
}
