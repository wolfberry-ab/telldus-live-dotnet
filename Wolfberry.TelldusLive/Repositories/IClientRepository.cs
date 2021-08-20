using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Client;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <summary>
    /// Handles operations for the client (the Telldus gateway/client/controller).
    /// A client controls devices and sensors.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Returns a list of clients owned by the current user (e.g. of type "TellStick ZNet Lite v2").
        /// </summary>
        /// <param name="extras">(optional) A comma-delimited list of extra information to fetch for each
        /// returned client. Currently supported fields are: coordinate, features, insurance,
        /// latestversion, suntime, timezone, transports and tzoffset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ClientsResponse> GetClientsAsync(
            string extras = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get client information
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="uuid">Optional</param>
        /// <param name="code">Optional</param>
        /// <param name="extras">Optional</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ClientInfoResponse> GetClientInfoAsync(
            string clientId,
            string uuid = null,
            string code = null,
            string extras = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Register an unregistered client to the calling user. Not yet implemented.
        /// </summary>
        /// <param name="clientId">Unique ID for the client</param>
        /// <param name="uuid">The specific UUID for the client</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RegisterAsync(
            string clientId,
            string uuid,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Removes a client from the user. The client needs to be activated again in order to be used.
        /// Not yet implemented.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveAsync(
            string clientId,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Sets the coordinates where the client is located.
        /// This can be used for calculating for example sunset and sunrise times.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetCoordinatesAsync(
            string clientId, 
            double longitude, 
            double latitude,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Rename client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string clientId, 
            string name,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Enable or disable push from this client.
        /// The current API key must be configured for push for this to work.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="enablePush"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> EnablePushAsync(
            string clientId, 
            bool enablePush,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Set the timezone where the client is located.
        /// Set timezone to an empty string to let the timezone be auto-detected.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="timezone"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetTimezoneAsync(
            string clientId, 
            string timezone,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Transfer a client to a new account. The receiver must verify the transfer.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="email"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> TransferAsync(
            string clientId,
            string email,
            string format = Constraints.JsonFormat);
    }
}