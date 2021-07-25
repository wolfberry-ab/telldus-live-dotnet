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
        
        public async Task<EventsResponse> GetEventsAsync(
            bool eventsOnly,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/events/list");

            if (eventsOnly)
            {
                // Can only be 1 or not set at all
                urlBuilder.AddQuery("listOnly", 1);
            }

            var url = urlBuilder.Build();

            return await GetOrThrow<EventsResponse>(url);
        }

        public async Task<EventGroupsResponse> GetEventGroupListAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/groupList");

            var url = urlBuilder.Build();

            return await GetOrThrow<EventGroupsResponse>(url);
        }

        public async Task<EventInfoResponse> GetEventInfoAsync(string eventId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/info");

            urlBuilder.AddQuery("id", eventId);

            var url = urlBuilder.Build();

            return await GetOrThrow<EventInfoResponse>(url);
        }

        public async Task<StatusResponse> RemoveActionAsync(string actionId, string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/removeAction");

            urlBuilder.AddQuery("id", actionId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveConditionAsync(
            string conditionId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/removeCondition");

            urlBuilder.AddQuery("id", conditionId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveEventAsync(
            string eventId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/removeEvent");

            urlBuilder.AddQuery("id", eventId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/removeGroup");

            urlBuilder.AddQuery("id", groupId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveTriggerAsync(
            string triggerId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/removeTrigger");

            urlBuilder.AddQuery("id", triggerId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<CreatedResponse> SetBlockHeaterTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            int hour,
            int minute,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setBlockHeaterTrigger");

            urlBuilder.AddOptionalQuery("id", eventId);
            urlBuilder.AddQuery("sensorId", sensorId);
            urlBuilder.AddQuery("hour", hour);
            urlBuilder.AddQuery("minute", minute);
            urlBuilder.AddOptionalQuery("triggerId", triggerId);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
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
            string format = ResponseFormat.JsonFormat)
        {
            if (repeats < 1 || repeats > 10)
            {
                throw new ArgumentException($"Parameter repeat must be 1-10. Value is {repeats}");
            }

            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setDeviceAction");

            urlBuilder.AddOptionalQuery("actionId", actionId);
            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("deviceID", deviceId);
            urlBuilder.AddQuery("method", (int)method);
            urlBuilder.AddOptionalQuery("value", value);
            urlBuilder.AddQuery("repeats", repeats);
            urlBuilder.AddOptionalQuery("delay", delayInSeconds);
            urlBuilder.AddOptionalQuery("delayPolicy", delayPolicy);
            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetDeviceConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string deviceId,
            DeviceMethod method,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setDeviceCondition");

            urlBuilder.AddOptionalQuery("id", conditionId);
            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("group", group);
            urlBuilder.AddQuery("deviceId", deviceId);
            urlBuilder.AddQuery("method", (int)method);
            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetDeviceTriggerAsync(
            string triggerId,
            string eventId,
            string deviceId,
            DeviceMethod method,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setDeviceTrigger");

            urlBuilder.AddOptionalQuery("id", triggerId);
            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("deviceId", deviceId);
            urlBuilder.AddQuery("method", (int)method);
            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetEmailActionAsync(
            string actionId,
            string eventId,
            string emailAddress,
            string message,
            int? delay,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setEmailAction");

            urlBuilder.AddOptionalQuery("id", actionId);
            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddAsEscapedQuery("address", emailAddress);
            urlBuilder.AddAsEscapedQuery("message", message);
            urlBuilder.AddOptionalQuery("delay", delay);
            urlBuilder.AddQuery("delayPolicy", delayPolicy);
            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetEventAsync(
            string eventId,
            string group,
            string description,
            bool active,
            int minRepeatInterval = 30,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setEvent");

            urlBuilder.AddOptionalQuery("id", eventId);
            urlBuilder.AddQuery("group", group);
            urlBuilder.AddAsEscapedQuery("description", description);
            urlBuilder.AddQuery("active", active);
            urlBuilder.AddQuery("minRepeatInterval", minRepeatInterval);
            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetGroupAsync (
            string groupId,
            string name,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setGroup");

            urlBuilder.AddOptionalQuery("id", groupId);
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
            string format = ResponseFormat.JsonFormat)
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
            urlBuilder.AddQuery("setAlways", setAlways);

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
            string format = ResponseFormat.JsonFormat)
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
            string format = ResponseFormat.JsonFormat)
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
            string format = ResponseFormat.JsonFormat)
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
                urlBuilder.AddQuery("delay", delayInSeconds.Value);
            }

            urlBuilder.AddQuery("delayPolicy", delayPolicy);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetSmsActionAsync(
            string actionId,
            string eventId,
            string to,
            string message,
            bool flash,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setSMSAction");

            if (actionId != null)
            {
                urlBuilder.AddQuery("id", actionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            // TODO: Add phone number validation
            urlBuilder.AddQuery("to", to);
            urlBuilder.AddAsEscapedQuery("message", message);
            urlBuilder.AddQuery("flash", flash);

            if (delayInSeconds != null)
            {
                urlBuilder.AddQuery("delay", delayInSeconds.Value);
            }

            urlBuilder.AddQuery("delayPolicy", delayPolicy);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetSensorConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string sensorId,
            bool value,
            Edge edge,
            string valueType,
            string scale = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setSensorCondition");

            if (conditionId != null)
            {
                urlBuilder.AddQuery("id", conditionId);
            }

            urlBuilder.AddQuery("eventId", eventId);

            if (group != null)
            {
                urlBuilder.AddQuery("group", group);
            }

            urlBuilder.AddQuery("sensorId", sensorId);
            urlBuilder.AddQuery("value", value);
            urlBuilder.AddQuery("edge", (int) edge);
            urlBuilder.AddQuery("valueType", valueType);

            if (scale != null)
            {
                urlBuilder.AddQuery("scale", scale);
            }

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetSensorTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            string value,
            Edge edge,
            string valueType,
            string scale = null,
            int reloadValue = 1,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setSensorTrigger");

            if (triggerId != null)
            {
                urlBuilder.AddQuery("id", triggerId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("sensorId", sensorId);
            urlBuilder.AddQuery("value", value);
            urlBuilder.AddQuery("edge", (int) edge);
            urlBuilder.AddQuery("valueType", valueType);
            urlBuilder.AddQuery("reloadValue", reloadValue);

            if (scale != null)
            {
                urlBuilder.AddQuery("scale", scale);
            }

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetSuntimeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            SunStatus sunStatus,
            int sunriseOffset,
            int sunsetOffset,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setSuntimeCondition");

            if (conditionId != null)
            {
                urlBuilder.AddQuery("id", conditionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("group", group);
            urlBuilder.AddQuery("sunStatus", (int)sunStatus);
            urlBuilder.AddQuery("sunriseOffset", sunriseOffset);
            urlBuilder.AddQuery("sunsetOffset", sunsetOffset);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetSuntimeTriggerAsync(
            string triggerId,
            string eventId,
            string clientId,
            SunStatus sunStatus,
            int offset,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setSuntimeTrigger");

            if (triggerId != null)
            {
                urlBuilder.AddQuery("id", triggerId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddQuery("clientId", clientId);
            urlBuilder.AddQuery("sunStatus", (int)sunStatus);
            urlBuilder.AddQuery("offset", offset);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetTimeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            int fromHour,
            int fromMinute,
            int toHour,
            int toMinute,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setTimeCondition");

            if (conditionId != null)
            {
                urlBuilder.AddQuery("id", conditionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            // TODO: What is the correct format of a group? What is a group?
            urlBuilder.AddQuery("group", group);
            urlBuilder.AddQuery("fromHour", fromHour);
            urlBuilder.AddQuery("fromMinute", fromMinute);
            urlBuilder.AddQuery("toHour", toHour);
            urlBuilder.AddQuery("toMinute", toMinute);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetUrlActionAsync(
            string actionId,
            string eventId,
            string urlCallback,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setURLAction");

            if (actionId != null)
            {
                urlBuilder.AddQuery("id", actionId);
            }

            urlBuilder.AddQuery("eventId", eventId);

            urlBuilder.AddAsEscapedQuery("url", urlCallback);

            if (delayInSeconds != null)
            {
                urlBuilder.AddQuery("delay", delayInSeconds.Value);
            }

            urlBuilder.AddQuery("delayPolicy", delayPolicy);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }

        public async Task<CreatedResponse> SetWeekdayConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string weekdays,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/event/setWeekdaysCondition");

            if (conditionId != null)
            {
                urlBuilder.AddQuery("id", conditionId);
            }

            urlBuilder.AddQuery("eventId", eventId);
            urlBuilder.AddAsEscapedQuery("group", group);
            urlBuilder.AddQuery("weekdays", weekdays);

            var url = urlBuilder.Build();

            return await GetOrThrow<CreatedResponse>(url);
        }
    }
}
