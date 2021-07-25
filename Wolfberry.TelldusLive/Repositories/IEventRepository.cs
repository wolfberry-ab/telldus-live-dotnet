using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Event;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IEventRepository
    {
        /// <summary>
        /// Get all events
        /// </summary>
        /// <param name="eventsOnly">Set to true to only list events
        /// (ignore triggers, conditions and actions), set to false to list all</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventsResponse> GetEventsAsync(
            bool eventsOnly,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get event groups
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventGroupsResponse> GetEventGroupListAsync(
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get info about an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventInfoResponse> GetEventInfoAsync(
            string eventId, 
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove an action
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveActionAsync(
            string actionId, 
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove a condition
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveConditionAsync(
            string conditionId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove event
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveEventAsync(
            string eventId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove trigger
        /// </summary>
        /// <param name="triggerId">Trigger ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveTriggerAsync(
            string triggerId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Adds or update a new sensor as trigger to an event
        /// </summary>
        /// <param name="triggerId">Trigger ID. Set to null to create new trigger.</param>
        /// <param name="eventId">Event ID to add to the trigger</param>
        /// <param name="sensorId">Sensor ID to monitor</param>
        /// <param name="hour">Departure time hour</param>
        /// <param name="minute">Departure time minute</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetBlockHeaterTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            int hour,
            int minute,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update device as an action to an event
        /// </summary>
        /// <param name="actionId">Action ID. Set to null to create new action</param>
        /// <param name="eventId">Event ID to add the action to</param>
        /// <param name="deviceId">The device ID to control</param>
        /// <param name="method">Method to run</param>
        /// <param name="value">Method value if needed by the method</param>
        /// <param name="repeats">Number of repeats (1-10)</param>
        /// <param name="delayInSeconds">Delay in seconds before executing the action. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetDeviceActionAsync(
            string actionId,
            string eventId,
            string deviceId,
            DeviceMethod method,
            string value,
            int repeats,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a device as condition to an event
        /// </summary>
        /// <param name="conditionId">Condition ID. Set to null to create new.</param>
        /// <param name="eventId">Event ID to add the trigger to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="deviceId">Device ID to query</param>
        /// <param name="method">The state the device must be in</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetDeviceConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string deviceId,
            DeviceMethod method,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a device as trigger to event
        /// </summary>
        /// <param name="triggerId">Trigger ID. Set to null to create new.</param>
        /// <param name="eventId"></param>
        /// <param name="deviceId"></param>
        /// <param name="method"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetDeviceTriggerAsync(
            string triggerId,
            string eventId,
            string deviceId,
            DeviceMethod method,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update e-mail action to an event.
        /// </summary>
        /// <param name="actionId">Action ID. Set to null to create new.</param>
        /// <param name="eventId">Event ID to set an action to</param>
        /// <param name="emailAddress">E-mail address to receiver</param>
        /// <param name="message">E-mail message</param>
        /// <param name="delay">Delay in seconds. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetEmailActionAsync(
            string actionId,
            string eventId,
            string emailAddress,
            string message,
            int? delay,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update an event
        /// </summary>
        /// <param name="eventId">Event ID. Set to null to create a new event</param>
        /// <param name="group">Group to add this event to. Set to 00000000-0000-0000-0000-000000000000
        /// to remove it from a group</param>
        /// <param name="description">User friendly description of the event</param>
        /// <param name="active">true to activate, false to pause</param>
        /// <param name="minRepeatInterval">Minimum time until next event can be run. Default is 30 seconds.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetEventAsync(
            string eventId,
            string group,
            string description,
            bool active,
            int minRepeatInterval = 30,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update an event group
        /// </summary>
        /// <param name="groupId">Group ID to update. Set to null to create a new.</param>
        /// <param name="name">Group name</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetGroupAsync(
            string groupId,
            string name,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a mode action
        /// </summary>
        /// <param name="actionId">Action ID. Set to null to create a new.</param>
        /// <param name="eventId">The event to add the action to</param>
        /// <param name="objectId">The ID of the object to change</param>
        /// <param name="objectType">The type of object. Only room is currently supported.</param>
        /// <param name="modeId">Mode ID</param>
        /// <param name="setAlways">Set mode again even if it's already set</param>
        /// <param name="delay">Number of seconds before running the action. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetModeActionAsync(
            string actionId,
            string eventId,
            string objectId,
            string objectType,
            string modeId,
            bool setAlways,
            int? delay,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a condition to an event
        /// </summary>
        /// <param name="conditionId">Condition ID. Set to null to create new.</param>
        /// <param name="eventId">Event ID to add the trigger to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="objectId">The object ID to query</param>
        /// <param name="objectType">The type of object. Only room is currently supported.</param>
        /// <param name="modeId">The mode the object must be set to</param>
        /// <param name="equalTo">ModeId must match (as opposed to Mode can be anything but modeId)</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetModeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string objectId,
            string objectType,
            string modeId,
            bool equalTo = true,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a mode as trigger to an event
        /// </summary>
        /// <param name="triggerId">Trigger ID. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the trigger to</param>
        /// <param name="objectType">Which type of object to listen for. Currently only room is supported</param>
        /// <param name="objectId">The id for the object to listen for. For rooms this is the room id</param>
        /// <param name="modeId">Mode ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetModeTriggerAsync(
            string triggerId,
            string eventId,
            string objectType,
            string objectId,
            string modeId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update a "push to mobile device" action
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="eventId"></param>
        /// <param name="phoneId">ID of mobile device</param>
        /// <param name="message"></param>
        /// <param name="delayInSeconds">Delay in seconds before executing the action. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetPushTriggerAsync(
            string actionId,
            string eventId,
            string phoneId,
            string message,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update an SMS Action
        /// </summary>
        /// <param name="actionId">Action ID to update. Set to null to create new.</param>
        /// <param name="eventId">Event ID to add the action to</param>
        /// <param name="to">The phone number in international format (e.g. +46708334455 for Sweden)</param>
        /// <param name="message">Message to send. Max 160 characters.</param>
        /// <param name="flash">true if it's a flash message, otherwise false</param>
        /// <param name="delayInSeconds">Delay in seconds before executing the action. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetSmsActionAsync(
            string actionId,
            string eventId,
            string to,
            string message,
            bool flash,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update sensor condition
        /// </summary>
        /// <param name="conditionId">Condition ID to update. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the condition to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="sensorId">Sensor ID to query</param>
        /// <param name="value">Value to trigger on</param>
        /// <param name="edge">Rising, equal or falling edge</param>
        /// <param name="valueType">The type of the value.
        /// Can be barpress (barometric pressure), co (carbon monoxide),
        /// co2 (carbon dioxide), dewp (dew point), genmeter (generic meter),
        /// humidity, loudness, lum (luminance), moisture, particulatematter2.5,
        /// rrate (rain rate), rtot (total rain), temp (temperature), uv (UV index),
        /// volume, watt (power), wavg (wind average), wgust (wind gust), weight and unknown</param>
        /// <param name="scale">Optional. Scale unit type</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetSensorConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string sensorId,
            bool value,
            Edge edge,
            string valueType,
            string scale = null,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update sensor trigger
        /// </summary>
        /// <param name="triggerId">Trigger ID to update. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the trigger to</param>
        /// <param name="sensorId">Sensor ID to monitor</param>
        /// <param name="value">The value to trigger on</param>
        /// <param name="edge">Any value of <see cref="Edge"/></param>
        /// <param name="valueType">The type of the value.
        /// Can be barpress (barometric pressure), co (carbon monoxide),
        /// co2 (carbon dioxide), dewp (dew point), genmeter (generic meter),
        /// humidity, loudness, lum (luminance), moisture, particulatematter2.5,
        /// rrate (rain rate), rtot (total rain), temp (temperature), uv (UV index),
        /// volume, watt (power), wavg (wind average), wgust (wind gust), weight and unknown</param>
        /// <param name="scale">Optional. Scale unit type</param>
        /// <param name="reloadValue">Optional. This value sets how much the value must drift
        /// before the trigger could be triggered again. This is useful for sensors that swings
        /// in the temperature. Default value is one degree.
        /// Example: If the trigger is set to 25 degree and reloadValue is 1, then the
        /// temperature needs to reach below 24 or above 26 for this trigger to trigger again.
        /// Must be in the interval 0.1 - 15</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetSensorTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            string value,
            Edge edge,
            string valueType,
            string scale = null,
            int reloadValue = 1,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update the sun as condition to an event
        /// </summary>
        /// <param name="conditionId">Condition ID. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the condition to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="sunStatus">See <see cref="SunStatus"/></param>
        /// <param name="sunriseOffset">Number of minutes before or after the sunrise</param>
        /// <param name="sunsetOffset">Number of minutes before or after the sunset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetSuntimeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            SunStatus sunStatus,
            int sunriseOffset,
            int sunsetOffset,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update suntime trigger to an event
        /// </summary>
        /// <param name="triggerId">Trigger ID to update. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the trigger to</param>
        /// <param name="clientId">The id of the client to use the sunset/sunrise time from</param>
        /// <param name="sunStatus">See <see cref="SunStatus"/></param>
        /// <param name="offset">Minutes before (positive number) or after (use negative number) sunrise/sunset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetSuntimeTriggerAsync(
            string triggerId,
            string eventId,
            string clientId,
            SunStatus sunStatus,
            int offset,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update time condition to an event
        /// </summary>
        /// <param name="conditionId">Condition ID to update. Set null to create new</param>
        /// <param name="eventId">Event ID to set condition to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="fromHour">0-23</param>
        /// <param name="fromMinute">0-59</param>
        /// <param name="toHour">0-23</param>
        /// <param name="toMinute">0-59</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetTimeConditionAsync(
            string conditionId,
            string eventId,
            string group,
            int fromHour,
            int fromMinute,
            int toHour,
            int toMinute,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update URL action
        /// </summary>
        /// <param name="actionId">Action ID to update. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the action to</param>
        /// <param name="urlCallback">URL to call on trigger</param>
        /// <param name="delayInSeconds">Delay in seconds before executing the action. Requires Premium.</param>
        /// <param name="delayPolicy">Only valid if a delay is set.
        /// "restart" restarts the timer, "continue" second activation is ignored and first
        /// timer continues to run.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetUrlActionAsync(
            string actionId,
            string eventId,
            string urlCallback,
            int? delayInSeconds,
            string delayPolicy,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update weekday condition
        /// </summary>
        /// <param name="conditionId">Condition ID to update. Set to null to create new</param>
        /// <param name="eventId">Event ID to add the condition to</param>
        /// <param name="group">The condition group to add this condition to.
        /// All conditions in a group must be true for the action to happen.
        /// If this is not set or null a new group will be created.</param>
        /// <param name="weekdays">A comma separated list of weekdays. 1 is monday. Example: 1,2,3,4</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<CreatedResponse> SetWeekdayConditionAsync(
            string conditionId,
            string eventId,
            string group,
            string weekdays,
            string format = ResponseFormat.JsonFormat);
    }
}