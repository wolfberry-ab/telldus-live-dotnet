using System;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<CreatedResponse> SetBlockHeaterTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            int hour,
            int minute,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setBlockHeaterTrigger?eventId={eventId}";

            requestUri += $"&sensorId={sensorId}";
            requestUri += $"&hour={hour}&minute={minute}";

            if (triggerId != null)
            {
                // Update existing trigger
                requestUri += $"&id={triggerId}";
            }

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetDeviceActionAsync(
            string actionId,
            string eventId,
            string deviceId,
            DeviceMethod method,
            string value,
            int repeats,
            int? delayInSeconds,
            string delayPolicy,
            string format = Constraints.JsonFormat)
        {

            if (repeats < 1 || repeats > 10)
            {
                throw new ArgumentException($"Parameter repeat must be 1-10. Value is {repeats}");
            }

            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setDeviceAction?eventId={eventId}";

            requestUri += $"&deviceID={deviceId}";
            requestUri += $"&method={(int)method}";

            if (value != null)
            {
                requestUri += $"&value={value}";
            }

            requestUri += $"&repeats={repeats}&delay={delayInSeconds}&delayPolicy={delayPolicy}";

            if (actionId != null)
            {
                // Update existing action
                requestUri += $"&id={actionId}";
            }

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetDeviceConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string deviceId,
            DeviceMethod method,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setDeviceCondition?eventId={eventId}";

            requestUri += $"&group={group}";
            requestUri += $"&deviceId={deviceId}&method={(int)method}";

            if (conditionId != null)
            {
                // Update existing condition
                requestUri += $"&id={conditionId}";
            }

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetDeviceTriggerAsync(
            string triggerId,
            string eventId,
            string deviceId,
            DeviceMethod method,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setDeviceTrigger?eventId={eventId}";

            requestUri += $"&deviceId={deviceId}";
            requestUri += $"&method={(int)method}";

            if (triggerId != null)
            {
                // Update existing trigger
                requestUri += $"&id={triggerId}";
            }

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetEmailActionAsync(
            string actionId,
            string eventId,
            string emailAddress,
            string message,
            int? delay,
            string delayPolicy,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setEmailAction?eventId={eventId}";

            var escapedEmailAddress = Uri.EscapeDataString(emailAddress);
            requestUri += $"&address={escapedEmailAddress}";

            var escapedMessage = Uri.EscapeDataString(message);
            requestUri += $"&message={escapedMessage}";

            if (delay != null)
            {
                requestUri += $"&delay={delay}";
            }

            requestUri += $"&delayPolicy={delayPolicy}";

            if (actionId != null)
            {
                // Update existing action
                requestUri += $"&id={actionId}";
            }

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetEventAsync(
            string eventId,
            string group,
            string description,
            bool active,
            int minRepeatInterval = 30,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/event/setEvent?group={group}";

            if (eventId != null)
            {
                // Update existing event
                requestUri += $"&id={eventId}";
            }

            var escapedDescription = Uri.EscapeDataString(description);
            requestUri += $"&description={escapedDescription}";

            requestUri += $"&minRepeatInterval={minRepeatInterval}";

            var activeNumber = active ? 1 : 0;
            requestUri += $"&active={activeNumber}";

            return await GetOrThrow<CreatedResponse>(requestUri);
        }

        public async Task<CreatedResponse> SetGroupAsync (
            string groupId,
            string name,
            string format = Constraints.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setGroup");

            if (groupId != null)
            {
                urlBuilder.AddQuery("id", groupId);
            }

            urlBuilder.AddAsEscapedQuery("name", name);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetModeActionAsync(
            string actionId,
            string eventId,
            string objectId,
            string objectType,
            string modeId,
            bool setAlways,
            int? delay,
            string delayPolicy,
            string format = Constraints.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setModeAction");

            if (actionId != null)
            {
                urlBuilder.AddQuery("id", actionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("objectId", objectId);
            urlBuilder.AddQuery("objectType", objectType);
            urlBuilder.AddQuery("modeId", modeId);
            urlBuilder.AddQueryAsInt("setAlways", setAlways);

            if (delay != null)
            {
                urlBuilder.AddQuery("delay", delay.ToString());
            }
            urlBuilder.AddQuery("delayPolicy", delayPolicy);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetModeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string objectId,
            string objectType,
            string modeId,
            bool equalTo = true,
            string format = Constraints.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setModeCondition");

            if (conditionId != null)
            {
                urlBuilder.AddQuery("id", conditionId);
            }

            urlBuilder.AddQuery("eventId", eventId);

            if (group != null)
            {
                urlBuilder.AddQuery("group", group);
            }

            urlBuilder.AddQuery("objectId", objectId);
            urlBuilder.AddQuery("objectType", objectType);
            urlBuilder.AddQuery("modeId", modeId);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetModeTriggerAsync(
            string triggerId,
            string eventId,
            string objectType,
            string objectId,
            string modeId,
            string format = Constraints.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setModeTrigger");

            if (triggerId != null)
            {
                urlBuilder.AddQuery("id", triggerId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("objectType", objectType);
            urlBuilder.AddQuery("objectId", objectId);
            urlBuilder.AddQuery("modeId", modeId);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetPushTriggerAsync(
            string actionId,
            string eventId,
            string phoneId,
            string message,
            int? delayInSeconds,
            string delayPolicy,
            string format = Constraints.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setPushAction");

            if (actionId != null)
            {
                urlBuilder.AddQuery("id", actionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("phoneId", phoneId);
            urlBuilder.AddAsEscapedQuery("message", message);

            if (delayInSeconds != null)
            {
                urlBuilder.AddQuery("delay", delayInSeconds.ToString());
            }

            urlBuilder.AddQuery("delayPolicy", delayPolicy);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }
    }
}
