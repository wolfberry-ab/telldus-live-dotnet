using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Adds a new group with devices and connects it to a client.
        /// The client must be editable for this to work.
        /// Please note that groups are devices as well.
        /// This means that all device commands will work for groups too.
        /// This command is deprecated. Use device/add instead.
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="name">Group name</param>
        /// <param name="devices">Comma separated string with the device ids this group should control</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> AddGroupAsync(
            string clientId,
            string name,
            string devices,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = ResponseFormat.JsonFormat);
    }
}
