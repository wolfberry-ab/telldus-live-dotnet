using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public class DeviceRepository
    {
        private readonly TelldusClient _client;

        public DeviceRepository(TelldusClient client)
        {
            _client = client;
        }

        // TODO: device/add

        public async Task<StatusResponse> BellAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/bell?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/command

        public async Task<StatusResponse> DimAsync(
            string deviceId, 
            [Range(0, 255)] int level,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/dim?id={deviceId}&level={level}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> DownAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/down?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/history

        // TODO: device/info

        public async Task<StatusResponse> LearnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/learn?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> RemoveAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/remove?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/rgb

        // TODO: device/setIgnore

        // TODO: device/setName

        // TODO: device/setModel

        // TODO: device/setMetadata

        // TODO: device/setProtocol

        // TODO: device/setParameter

        public async Task<StatusResponse> StopAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/stop?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/thermostat

        public async Task<StatusResponse> TurnOnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/turnOn?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> TurnOffAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/turnOff?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> Upsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/device/up?id={deviceId}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }
    }
}
