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
        /// <param name="format"></param>
        /// <returns></returns>
        Task<EventGroupsResponse> GetEventGroupListAsync(
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get info about an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<EventInfoResponse> GetEventInfoAsync(
            string eventId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove an action
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> RemoveActionAsync(
            string actionId, 
            string format = Constraints.JsonFormat);

        // TODO: removeEvent

        // TODO: removeGroup

        // TODO: removeTrigger

        // TODO: setBlockHeaterTrigger

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