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
        /// <param name="listOnly">Set to "1" or null</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventsResponse> GetEventsAsync(
            string listOnly, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get event groups
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventGroupsResponse> GetEventGroupListAsync(
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get info about an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EventInfoResponse> GetEventInfoAsync(
            string eventId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove an action
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveActionAsync(
            string actionId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove a condition
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveConditionAsync(
            string conditionId,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove event
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveEventAsync(
            string eventId,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove trigger
        /// </summary>
        /// <param name="triggerId">Trigger ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveTriggerAsync(
            string triggerId,
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

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
            string format = Constraints.JsonFormat);

        // TODO: setModeAction

        // TODO: setModeCondition

        // TODO: setModeTrigger

        // TODO: setPushAction

        // TODO: setSMSAction

        // TODO: setSensorCondition

        // TODO: setSensorTrigger

        // TODO: setSuntimeCondition

        // TODO: setSuntimeTrigger

        // TODO: setTimeCondition

        // TODO: setTimeTrigger

        // TODO: setURLAction

        // TODO: setWeekdaysCondition
    }
}