using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Sensor;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class SensorRepositoryTests
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
        public async Task GetSensorsAsync_ReturnsSensors()
        {
            const string expectedId = "456";
            var mockedResponse = new TelldusSensorsResponse
            {
                Sensor = new List<Sensor>
                {
                    new Sensor { Id = expectedId }
                }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.GetSensorsAsync(false, true);

            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Sensors.First().Id);
        }

        [Fact]
        public async Task GetSensorInfoAsync_ReturnsSensorInfo()
        {
            var mockedResponse = new SensorResponse { Id = "789", Name = "Outdoor" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.GetSensorInfoAsync("789", true);

            Assert.NotNull(result);
            Assert.Equal("789", result.Id);
            Assert.Equal("Outdoor", result.Name);
        }

        [Fact]
        public async Task SetNameAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.SetNameAsync("123", "New Name");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task IgnoreAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.IgnoreAsync("123", true);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetSensorsAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Access denied" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetSensorsAsync(false, false));
        }

        [Fact]
        public async Task GetSensorsAsync_NullResponse_ThrowsException()
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            ISensorRepository repository = new SensorRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetSensorsAsync(false, false));
        }

        [Fact]
        public async Task GetHistoryAsync_ReturnsHistory()
        {
            var mockedResponse = new SensorHistoryResponse
            {
                History = new List<HistoryEntry>
                {
                    new HistoryEntry { Ts = 1000, Uuid = "uuid-1" }
                }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.GetHistoryAsync("123", true, true, true);

            Assert.NotNull(result);
            Assert.Single(result.History);
        }

        [Fact]
        public async Task RemoveHistoryAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.RemoveHistoryAsync("123");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveValueAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.RemoveValueAsync("123", "uuid-abc");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task ResetMaxMin_WithNullType_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.ResetMaxMin("123", null);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task ResetMaxMin_WithType_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.ResetMaxMin("123", "temperature");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetKeepHistoryAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISensorRepository repository = new SensorRepository(client);

            var result = await repository.SetKeepHistoryAsync("123", true);

            Assert.Equal("success", result.Status);
        }
    }
}
