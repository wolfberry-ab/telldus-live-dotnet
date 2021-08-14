using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            const string url = "http://dummy.url";
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.GetResponseAsType<ClientsResponse>(url)
                .ReturnsForAnyArgs(mockedClients);

            IClientRepository repository = new ClientRepository(telldusClient);

            var clients = await repository.GetClientsAsync();

            Assert.NotNull(clients);
            Assert.Equal(expectedId, clients.Client.First().Id);
        }
    }
}
