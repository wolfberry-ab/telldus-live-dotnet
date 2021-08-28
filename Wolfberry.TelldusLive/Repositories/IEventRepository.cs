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
        /// <param name="triggerId">Trigger ID. Leave empty to create new trigger.</param>
        /// <param name="eventId">Event ID to add to the trigger</param>
        /// <param name="sensorId">Sensor ID to monitor</param>
        /// <param name="hour">Departure time hour</param>
        /// <param name="minute">Departure time minute</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetBlockHeaterTriggerAsync(
            string triggerId,
            string eventId,
            string sensorId,
            int hour,
            int minute,
            string format = Constraints.JsonFormat);

        // TODO: setDeviceAction

        // TODO: setDeviceCondition

        // TODO: setDeviceTrigger

        // TODO: setEmailAction

        // TODO: setEvent

        // TODO: setGroup

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