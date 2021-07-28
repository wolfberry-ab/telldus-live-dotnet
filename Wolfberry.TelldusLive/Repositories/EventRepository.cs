using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public class EventRepository
    {
        private readonly TelldusClient _client
            ;

        public EventRepository(TelldusClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get events
        /// </summary>
        /// <param name="listOnly">Set to "1" or null</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<EventsResponse> GetEventsAsync(string listOnly, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/events/list";

            if (listOnly != null)
            {
                const string listOnlyValue = "1";
                if (!listOnlyValue.Equals(listOnly))
                {
                    throw new ArgumentException("listOnly parameter can only be \"1\" or null");
                }
                requestUri += $"?listOnly={listOnly}";
            }

            var response = await _client.GetResponseAsType<EventsResponse>(requestUri);

            return response;
        }
    }
}
