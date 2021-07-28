using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public class SensorRepository
    {
        private readonly TelldusClient _client;

        public SensorRepository(TelldusClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Returns a list of all sensors associated with the current user
        /// https://api.telldus.net/explore/sensors/list
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<IList<Sensor>> GetSensorsAsync(string format = Constraints.JsonFormat)
        {
            // TODO: Add all parameters
            var requestUri = $"{_client.BaseUrl}/{format}/sensors/list?includeValues=1";

            var response = await _client.GetResponseAsType<SensorsResponse>(requestUri);

            return response.Sensor;
        }
    }
}
