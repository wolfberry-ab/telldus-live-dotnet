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

        public async Task<StatusResponse> AddAsync(
            string clientId,
            string name,
            string transport,
            string protocol,
            string model,
            string parameters,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/add");

            // NOTE: the only route that use "clientId" as parameter name instead of "id"
            urlBuilder.AddQuery("clientId", clientId);
            urlBuilder.AddAsEscapedQuery("name", name);
            urlBuilder.AddQuery("transport", transport);
            urlBuilder.AddQuery("protocol", protocol);
            urlBuilder.AddQuery("model", model);
            urlBuilder.AddQuery("parameters", parameters);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> BellAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/bell");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SendCommandAsync(
            string deviceId,
            DeviceMethod method,
            string value = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/command");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("id", (int)method);
            urlBuilder.AddOptionalQuery("value", value);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> DimAsync(string deviceId, int level, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/dim");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("level", level);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> DownAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/down");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<HistoryResponse> GetHistoryAsync(
            string deviceId,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            bool? lastFirst = null,
            string extras = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/history");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddOptionalQuery("from", fromTimestamp.ToString());
            urlBuilder.AddOptionalQuery("to", toTimestamp.ToString());
            urlBuilder.AddOptionalQuery("lastFirst", lastFirst);
            urlBuilder.AddOptionalQuery("extras", extras);

            var url = urlBuilder.Build();

            return await GetOrThrow<HistoryResponse>(url);
        }

        public async Task<DeviceResponse> GetDeviceInfoAsync(
            string deviceId,
            string uuid = null,
            string supportedMethods = null,
            string extras = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/info");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddOptionalQuery("uuid", uuid);
            urlBuilder.AddOptionalQuery("supportedMethods", supportedMethods);
            urlBuilder.AddOptionalQuery("extras", extras);

            var url = urlBuilder.Build();

            return await GetOrThrow<DeviceResponse>(url);
        }

        public async Task<StatusResponse> LearnAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/learn");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/remove");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetRgbAsync(string deviceId,
            int red,
            int green,
            int blue,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/rgb");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("red", red);
            urlBuilder.AddQuery("green", green);
            urlBuilder.AddQuery("blue", blue);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> IgnoreAsync(
            string deviceId,
            bool ignore,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setIgnore");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("ignore", ignore);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetNameAsync(
            string deviceId,
            string name,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setName");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddAsEscapedQuery("name", name);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetModelAsync(
            string deviceId,
            string model,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setModel");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("model", model);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetMetadataAsync(
            string deviceId,
            string parameter,
            string value,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setMetadata");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("parameter", parameter);
            urlBuilder.AddQuery("value", value);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetProtocolAsync(
            string deviceId,
            string protocol,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setProtocol");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("protocol", protocol);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetParameterAsync(
            string deviceId,
            string parameter,
            string value,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/setParameter");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("parameter", parameter);
            urlBuilder.AddQuery("value", value);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> StopAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/stop");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetThermostatAsync(
            string deviceId,
            string mode,
            string temperature,
            int? scale,
            int changeMode,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/thermostat");

            urlBuilder.AddQuery("id", deviceId);
            urlBuilder.AddQuery("mode", mode);
            urlBuilder.AddQuery("changeMode", changeMode);
            urlBuilder.AddOptionalQuery("temperature", temperature);
            urlBuilder.AddOptionalQuery("scale", scale);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> TurnOnAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/turnOn");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> TurnOffAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/turnOff");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> UpAsync(string deviceId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/device/up");

            urlBuilder.AddQuery("id", deviceId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<DevicesResponse> GetDevicesAsync(
            bool includeIgnored = false,
            string supportedMethods = null,
            string extras = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/devices/list");

            urlBuilder.AddQuery("includeIgnored", includeIgnored);
            urlBuilder.AddOptionalQuery("supportedMethods", supportedMethods);
            urlBuilder.AddOptionalQuery("extras", extras);

            var url = urlBuilder.Build();

            return await GetOrThrow<DevicesResponse>(url);
        }
    }
}
