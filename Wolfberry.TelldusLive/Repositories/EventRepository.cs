using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;
using Wolfberry.TelldusLive.ViewModels.Event;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IEventRepository
    {
        /// <summary>
        /// Get events
        /// </summary>
        /// <param name="listOnly">Set to "1" or null</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventsResponse> GetEventsAsync(string listOnly, string format = Constraints.JsonFormat);

        /// <summary>
        /// Get event groups
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<EventGroupsResponse> GetEventGroupListAsync(string format = Constraints.JsonFormat);

        /// <summary>
        /// Get info about an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<EventInfoResponse> GetEventInfoAsync(string eventId, string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove an action
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> RemoveActionAsync(string actionId, string format = Constraints.JsonFormat);
    }

    /// <inheritdoc cref="IEventRepository"/>
    public class EventRepository : IEventRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public EventRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<EventsResponse> GetEventsAsync(string listOnly, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/events/list";

            if (listOnly != null)
            {
                const string listOnlyValue = "1";
                if (!listOnlyValue.Equals(listOnly))
                {
                    throw new ArgumentException("listOnly parameter can only be \"1\" or null");
                }
                requestUri += $"?listOnly={listOnly}";
            }

            var response = await _httpClient.GetResponseAsType<EventsResponse>(requestUri);

            return response;
        }

        public async Task<EventGroupsResponse> GetEventGroupListAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/groupList";

            var response = await _httpClient.GetResponseAsType<EventGroupsResponse>(requestUri);

            // TODO: Handle possible error responses (404, time out)

            return response;
        }

        public async Task<EventInfoResponse> GetEventInfoAsync(string eventId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/info?id={eventId}";

            var responseJson = await _httpClient.GetAsJsonAsync(requestUri);

            var errorMessage = ErrorParser.GetOrCreateErrorMessage(responseJson);
            if (errorMessage != null)
            {
                throw new RepositoryException(errorMessage);
            }

            var response = JsonUtil.Deserialize<EventInfoResponse>(responseJson);
            return response;
        }

        public async Task<StatusResponse> RemoveActionAsync(string actionId, string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/removeAction?id={actionId}";

            var responseJson = await _httpClient.GetAsJsonAsync(requestUri);

            var errorMessage = ErrorParser.GetOrCreateErrorMessage(responseJson);
            if (errorMessage != null)
            {
                throw new RepositoryException(errorMessage);
            }

            var response = JsonUtil.Deserialize<StatusResponse>(responseJson);
            return response;
        }
    }
}
