using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Device;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        // TODO: Test
        public async Task<StatusResponse> AddAsync(
            string clientId, 
            string name, 
            string transport, 
            string protocol,
            string model, 
            string parameters, 
            string format = Constraints.JsonFormat)
        {
            var encodedName = Uri.EscapeDataString(name);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/add?clientId={clientId}";
            requestUri += $"&name={encodedName}&transport={transport}&protocol={protocol}&model={model}&parameters={parameters}";

            var responseJson = await _httpClient.GetAsJsonAsync(requestUri);

            var errorMessage = ErrorParser.GetOrCreateErrorMessage(responseJson);
            if (errorMessage != null)
            {
                throw new RepositoryException(errorMessage);
            }

            var response = JsonUtil.Deserialize<StatusResponse>(responseJson);
            return response;
        }

        public async Task<StatusResponse> BellAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/bell?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        // TODO: test
        public async Task<StatusResponse> SendCommandAsync(
            string deviceId,
            DeviceMethod method,
            string value = null,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/command?id={deviceId}&method={method}";

            if (value != null)
            {
                requestUri += $"&value={value}";
            }

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> DimAsync(string deviceId, int level, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/dim?id={deviceId}&level={level}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> DownAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/down?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<HistoryResponse> GetHistoryAsync(
            string deviceId,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            bool? lastFirst = null,
            string extras = null,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/history?id={deviceId}";

            if (fromTimestamp != null)
            {
                requestUri += $"&from={fromTimestamp}";
            }

            if (toTimestamp != null)
            {
                requestUri += $"&to={toTimestamp}";
            }

            if (lastFirst != null)
            {
                var lastFirstInteger = (bool) lastFirst ? 1 : 0;
                requestUri += $"&lastFirst={lastFirstInteger}";
            }

            if (extras != null)
            {
                requestUri += $"&extras={extras}";
            }

            return await GetOrThrow<HistoryResponse>(requestUri);
        }

        public async Task<DeviceResponse> GetDeviceInfo(
            string deviceId,
            string uuid = null,
            string supportedMethods = null,
            string extras = null,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/info?id={deviceId}";

            if (uuid != null)
            {
                requestUri += $"&uuid={uuid}";
            }

            if (supportedMethods != null)
            {
                requestUri += $"&supportedMethods={supportedMethods}";
            }

            if (extras != null)
            {
                requestUri += $"&extras={extras}";
            }

            var response = await _httpClient.GetResponseAsType<DeviceResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> LearnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/learn?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/remove?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
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

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        // TODO: device/setModel

        // TODO: device/setMetadata

        // TODO: device/setProtocol

        // TODO: device/setParameter

        public async Task<StatusResponse> StopAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/stop?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        // TODO: device/thermostat

        public async Task<StatusResponse> TurnOnAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/turnOn?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> TurnOffAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/turnOff?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> UpAsync(string deviceId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/device/up?id={deviceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
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
