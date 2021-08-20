using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Sensor;

namespace Wolfberry.TelldusLive.Repositories
{
    public class SensorRepository : BaseRepository, ISensorRepository
    {
        public SensorRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        /// <inheritdoc cref="ISensorRepository"/>
        public async Task<IList<Sensor>> GetSensorsAsync(string format = Constraints.JsonFormat)
        {
            // TODO: Add all parameters
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensors/list?includeValues=1";

            var response = await _httpClient.GetResponseAsType<SensorsResponse>(requestUri);

            return response.Sensor;
        }

        public async Task<SensorResponse> GetSensorInfoAsync(
            string sensorId,
            bool includeUnit,
            string format = Constraints.JsonFormat)

        {
            var includeUnitInteger = includeUnit ? 1 : 0;
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/info?id={sensorId}&includeUnit={includeUnitInteger}";

            return await GetOrThrow<SensorResponse>(requestUri);
        }

        public async Task<StatusResponse> IgnoreAsync(
            string sensorId,
            bool ignore,
            string format = Constraints.JsonFormat)
        {
            var ignoreInteger = ignore ? 1 : 0;
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/setIgnore?id={sensorId}&ignore={ignoreInteger}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetNameAsync(
            string sensorId,
            string name,
            string format = Constraints.JsonFormat)
        {
            var escapedName = Uri.EscapeDataString(name);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/setName?id={sensorId}&name={escapedName}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<SensorHistoryResponse> GetHistoryAsync(
            string sensorId,
            bool includeKey,
            bool includeUnit,
            bool includeHumanReadableDate,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/history?id={sensorId}";

            var includeKeyInteger = includeKey ? 1 : 0;
            var includeUnitInteger = includeUnit ? 1 : 0;

            requestUri +=
                $"&includeKey={includeKeyInteger}&includeUnit={includeUnitInteger}&includeHumanReadableDate={includeHumanReadableDate}";

            if (fromTimestamp != null)
            {
                requestUri += $"&from={fromTimestamp}";
            }

            if (toTimestamp != null)
            {
                requestUri += $"&to={toTimestamp}";
            }

            return await GetOrThrow<SensorHistoryResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveHistoryAsync(
            string sensorId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/removeHistory?id={sensorId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveValueAsync(
            string sensorId,
            string timeUuid,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/removeValue?id={sensorId}";

            requestUri += $"&TimeUUID={timeUuid}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> ResetMaxMin(
            string sensorId,
            string type = null,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/resetMaxMin?id={sensorId}";

            if (type == null)
            {
                type = string.Empty;
            }

            requestUri += $"&type={type}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetKeepHistoryAsync(
            string sensorId,
            bool keepHistory,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensor/setKeepHistory?id={sensorId}";

            var keepHistoryInteger = keepHistory ? 1 : 0;
            requestUri += $"&keepHistory={keepHistoryInteger}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
