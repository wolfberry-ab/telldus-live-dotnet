using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive
{
    public class TelldusClient
    {
        /// <summary>
        /// The actual call should be performed at: https://api.telldus.com/{format}/{function}
        ///  Where:
        ///  {format} is the returned format and should be either json or xml
        ///  {function} is the function to call.
        /// </summary>
        private const string BaseUrl = "http://api.telldus.com";
        private readonly Authenticator _authenticator;
        public const string JsonFormat = "json";
        public const string XmlFormat = "xml";

        public TelldusClient(Authenticator authenticator)
        {
            _authenticator = authenticator;
            _authenticator.InitializeHttpClient();
        }

        /// <summary>
        /// Returns a list of clients owned by the current user (e.g. of type "TellStick ZNet Lite v2").
        /// </summary>
        /// <param name="extras">(optional) A comma-delimited list of extra information to fetch for each
        /// returned client. Currently supported fields are: coordinate, features, insurance,
        /// latestversion, suntime, timezone, transports and tzoffset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<IList<Client>> GetClientsAsync(string extras, string format = JsonFormat)
        {

            var requestUri = $"{BaseUrl}/{format}/clients/list";

            if (extras != null)
            {
                requestUri += $"?extras={extras}";
            }

            var responseJson = await GetAsJsonStringAsync(requestUri);
            var response = JsonUtil.Deserialize<ClientsResponse>(responseJson);

            return response.Client;
        }

        /// <summary>
        /// Returns a list of all sensors associated with the current user
        /// https://api.telldus.net/explore/sensors/list
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<IList<Sensor>> GetSensorsAsync(string format = JsonFormat)
        {
            var requestUri = $"{BaseUrl}/{format}/sensors/list?includeValues=1";

            var responseJson = await GetAsJsonStringAsync(requestUri);
            var response = JsonUtil.Deserialize<SensorsResponse>(responseJson);

            return response.Sensor;
        }

        private async Task<string> GetAsJsonStringAsync(string uri)
        {
            var response = await _authenticator.HttpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Get events
        /// </summary>
        /// <param name="listOnly">Set to "1" or null</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<IList<Event>> GetEventsAsync(string listOnly, string format = JsonFormat)
        {
            var requestUri = $"{BaseUrl}/{format}/events/list";

            if (listOnly != null)
            {
                const string listOnlyValue = "1";
                if (!listOnlyValue.Equals(listOnly))
                {
                    throw new ArgumentException("listOnly parameter can only be \"1\" or null");
                }
                requestUri += $"?listOnly={listOnly}";
            }

            var responseJson = await GetAsJsonStringAsync(requestUri);
            var response = JsonUtil.Deserialize<EventsResponse>(responseJson);

            return response.Event;
        }
    }
}
