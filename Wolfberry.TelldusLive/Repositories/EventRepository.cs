using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Event;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <inheritdoc cref="IEventRepository"/>
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
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

        public async Task<StatusResponse> RemoveConditionAsync(
            string conditionId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/removeCondition?id={conditionId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveEventAsync(
            string eventId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/removeEvent?id={eventId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/removeGroup?id={groupId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveTriggerAsync(
            string triggerId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/removeTrigger?id={triggerId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetBlockHeaterTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            int hour,
            int minute,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setBlockHeaterTrigger?id={triggerId}";

            requestUri += $"&eventId={eventId}&sensorId={sensorId}";
            requestUri += $"&hour={hour}&minute={minute}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
