using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Device;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class DeviceRepositoryTests
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
        public async Task GetDevicesAsync_ReturnsDevices()
        {
            const string expectedId = "100";
            var mockedResponse = new DevicesResponse
            {
                Device = new List<Device>
                {
                    new Device { Id = expectedId, Name = "Lamp" }
                }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.GetDevicesAsync();

            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Device.First().Id);
        }

        [Fact]
        public async Task GetDeviceInfoAsync_ReturnsDeviceInfo()
        {
            var mockedResponse = new DeviceResponse { Id = "200", Name = "Switch" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.GetDeviceInfoAsync("200");

            Assert.Equal("200", result.Id);
            Assert.Equal("Switch", result.Name);
        }

        [Fact]
        public async Task TurnOnAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.TurnOnAsync("100");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task TurnOffAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.TurnOffAsync("100");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task DimAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.DimAsync("100", 128);

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            var result = await repository.RemoveAsync("100");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task GetDevicesAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Not found" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IDeviceRepository repository = new DeviceRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetDevicesAsync());
        }

        [Fact]
        public async Task GetDevicesAsync_NullResponse_ThrowsException()
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            IDeviceRepository repository = new DeviceRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetDevicesAsync());
        }
    }
}
