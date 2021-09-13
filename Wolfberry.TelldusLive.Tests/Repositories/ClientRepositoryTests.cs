using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using Wolfberry.TelldusLive.Models.Client;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class ClientRepositoryTests
    {
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
            const string url = "https://wolfberry.se:443/api";
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl
                .Returns(url);
            telldusClient.GetAsJsonAsync(default)
                .ReturnsForAnyArgs(JsonConvert.SerializeObject(mockedClients));

            IClientRepository repository = new ClientRepository(telldusClient);

            var clients = await repository.GetClientsAsync();

            Assert.NotNull(clients);
            Assert.Equal(expectedId, clients.Client.First().Id);
        }
    }
}
