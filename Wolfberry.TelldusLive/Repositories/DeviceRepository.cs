using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;
using Wolfberry.TelldusLive.ViewModels.Device;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IDeviceRepository
    {
        Task<StatusResponse> BellAsync(string deviceId, string format = Constraints.JsonFormat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="level">0-255</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> DimAsync(string deviceId, int level, string format = Constraints.JsonFormat);

        Task<StatusResponse> DownAsync(string deviceId, string format = Constraints.JsonFormat);
        Task<StatusResponse> LearnAsync(string deviceId, string format = Constraints.JsonFormat);
        Task<StatusResponse> RemoveAsync(string deviceId, string format = Constraints.JsonFormat);

        /// <summary>
        /// Rename device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="name"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string deviceId,
            string name,
            string format = Constraints.JsonFormat);
        Task<StatusResponse> StopAsync(string deviceId, string format = Constraints.JsonFormat);
        Task<StatusResponse> TurnOnAsync(string deviceId, string format = Constraints.JsonFormat);
        Task<StatusResponse> TurnOffAsync(string deviceId, string format = Constraints.JsonFormat);
        Task<StatusResponse> UpAsync(string deviceId, string format = Constraints.JsonFormat);

        Task<DevicesResponse> GetDevicesAsync(
            bool includeIgnored = false,
            string supportedMethods = null,
            string extras = null,
            string format = Constraints.JsonFormat);
    }

    public class DeviceRepository : IDeviceRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public DeviceRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // TODO: device/add

        public async Task<StatusResponse> BellAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/bell?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/command

        public async Task<StatusResponse> DimAsync(string deviceId, int level, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/dim?id={deviceId}&level={level}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> DownAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/down?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/history

        // TODO: device/info

        public async Task<StatusResponse> LearnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/learn?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> RemoveAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/remove?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/rgb

        // TODO: device/setIgnore

        public async Task<StatusResponse> SetNameAsync(
            string deviceId,
            string name,
            string format = Constraints.JsonFormat)
        {
            var encodedName = Uri.EscapeDataString(name);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/setName?id={deviceId}&name={encodedName}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/setModel

        // TODO: device/setMetadata

        // TODO: device/setProtocol

        // TODO: device/setParameter

        public async Task<StatusResponse> StopAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/stop?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        // TODO: device/thermostat

        public async Task<StatusResponse> TurnOnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/turnOn?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> TurnOffAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/turnOff?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> UpAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/up?id={deviceId}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<DevicesResponse> GetDevicesAsync(
            bool includeIgnored = false,
            string supportedMethods = null, 
            string extras = null,
            string format = Constraints.JsonFormat)
        {
            var includeIgnoredInteger = includeIgnored ? 1 : 0;
            var requestUri = $"{_httpClient.BaseUrl}/{format}/devices/list?includeIgnored={includeIgnoredInteger}";

            // TODO: Test this parameter
            if (supportedMethods != null)
            {
                requestUri += $"&supportedMethods={supportedMethods}";
            }

            if (extras != null)
            {
                requestUri += $"&extras={extras}";
            }

            var response = await _httpClient.GetResponseAsType<DevicesResponse>(requestUri);

            return response;
        }
    }
}
