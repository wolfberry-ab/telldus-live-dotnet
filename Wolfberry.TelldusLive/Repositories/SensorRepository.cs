using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Sensor;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public class SensorRepository : BaseRepository, ISensorRepository
    {
        public SensorRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        /// <inheritdoc cref="ISensorRepository"/>
        public async Task<SensorsResponse> GetSensorsAsync(
            bool includeIgnored,
            bool includeValues,
            bool? includeScale = null,
            bool? includeUnit = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensors/list");

            urlBuilder.AddQuery("includeIgnored", includeIgnored);
            urlBuilder.AddQuery("includeValues", includeValues);
            urlBuilder.AddOptionalQuery("includeScale", includeScale);
            urlBuilder.AddOptionalQuery("includeUnit", includeUnit);

            var url = urlBuilder.Build();

            var response = await GetOrThrow<TelldusSensorsResponse>(url);
            return new SensorsResponse(response.Sensor);
        }

        public async Task<SensorResponse> GetSensorInfoAsync(
            string sensorId,
            bool includeUnit,
            string format = ResponseFormat.JsonFormat)

        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/info");

            urlBuilder.AddQuery("id", sensorId);
            urlBuilder.AddQuery("includeUnit", includeUnit);

            var url = urlBuilder.Build();

            return await GetOrThrow<SensorResponse>(url);
        }

        public async Task<StatusResponse> IgnoreAsync(
            string sensorId,
            bool ignore,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/setIgnore");

            urlBuilder.AddQuery("id", sensorId);
            urlBuilder.AddQuery("ignore", ignore);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetNameAsync(
            string sensorId,
            string name,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/setName");

            urlBuilder.AddQuery("id", sensorId);
            urlBuilder.AddAsEscapedQuery("name", name);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<SensorHistoryResponse> GetHistoryAsync(
            string sensorId,
            bool includeKey,
            bool includeUnit,
            bool includeHumanReadableDate,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/history");

            urlBuilder.AddQuery("id", sensorId);
            urlBuilder.AddQuery("includeKey", includeKey);
            urlBuilder.AddQuery("includeUnit", includeUnit);
            urlBuilder.AddQuery("includeHumanReadableDate", includeHumanReadableDate);
            urlBuilder.AddOptionalQuery("fromTimestamp", fromTimestamp);
            urlBuilder.AddOptionalQuery("toTimestamp", toTimestamp);
            urlBuilder.AddOptionalQuery("fromTimestamp", fromTimestamp);

            var url = urlBuilder.Build();

            return await GetOrThrow<SensorHistoryResponse>(url);
        }

        public async Task<StatusResponse> RemoveHistoryAsync(
            string sensorId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/removeHistory");

            urlBuilder.AddQuery("id", sensorId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveValueAsync(
            string sensorId,
            string timeUuid,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/removeValue");

            urlBuilder.AddQuery("id", sensorId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> ResetMaxMin(
            string sensorId,
            string type = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/resetMaxMin");

            urlBuilder.AddQuery("id", sensorId);

            if (type == null)
            {
                type = string.Empty;
            }

            urlBuilder.AddQuery("type", type);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetKeepHistoryAsync(
            string sensorId,
            bool keepHistory,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/sensor/setKeepHistory");

            urlBuilder.AddQuery("id", sensorId);
            urlBuilder.AddQuery("keepHistory", keepHistory);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }
    }
}
