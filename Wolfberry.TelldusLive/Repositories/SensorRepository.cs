using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;

namespace Wolfberry.TelldusLive.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public SensorRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="ISensorRepository"/>
        public async Task<IList<Sensor>> GetSensorsAsync(string format = Constraints.JsonFormat)
        {
            // TODO: Add all parameters
            var requestUri = $"{_httpClient.BaseUrl}/{format}/sensors/list?includeValues=1";

            var response = await _httpClient.GetResponseAsType<SensorsResponse>(requestUri);

            return response.Sensor;
        }
    }
}
