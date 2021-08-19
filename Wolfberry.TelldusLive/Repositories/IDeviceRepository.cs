using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Device;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IDeviceRepository
    {
        /// <summary>
        /// Add device to client (gateway/controller)
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="name"></param>
        /// <param name="transport"></param>
        /// <param name="protocol"></param>
        /// <param name="model"></param>
        /// <param name="parameters"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> AddAsync(
            string clientId,
            string name,
            string transport,
            string protocol,
            string model,
            string parameters,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Send bell command to device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> BellAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Send a command to device
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <param name="method">Method constant</param>
        /// <param name="value">Method value if needed by the method</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SendCommandAsync(
            string deviceId,
            DeviceMethod method,
            string value = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Dim device to specified level
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="level">0-255</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> DimAsync(
            string deviceId, 
            int level, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Send down command to device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> DownAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get device state history. Telldus API is in BETA (2021-08-14).
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="fromTimestamp">Timestamp in seconds</param>
        /// <param name="toTimestamp">Timestamp in seconds</param>
        /// <param name="lastFirst">1 or 0</param>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned device. Currently supported fields are: timezone and tzoffset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<HistoryResponse> GetHistoryAsync(
            string deviceId,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            bool? lastFirst = null,
            string extras = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get device information
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="uuid">Optional. Use this for newly created devices without id</param>
        /// <param name="supportedMethods">	The methods supported by the calling application</param>
        /// <param name="extras">Optional. A comma-delimited list of extra information to fetch for each
        /// returned device. Currently supported fields are: coordinate, metadata, timezone, transport,
        /// and tzoffset</param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<DeviceResponse> GetDeviceInfo(
            string deviceId,
            string uuid = null,
            string supportedMethods = null,
            string extras = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Send a special learn command to some devices that need a special
        /// learn-command to be used from TellStick
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> LearnAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Remove device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> RemoveAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        // TODO: rgb

        // TODO: setIgnore

        // TODO: setName
        
        /// <summary>
        /// Rename device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="name"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string deviceId,
            string name,
            string format = Constraints.JsonFormat);


        // TODO: setModel

        // TODO: setMetadata

        // TODO: setProtocol

        // TODO: setParameter

        /// <summary>
        /// Stop device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> StopAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        // TODO: thermostat

        /// <summary>
        /// Turn on device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> TurnOnAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Turn off device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> TurnOffAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Send up command to device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> UpAsync(
            string deviceId, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Get all devices
        /// </summary>
        /// <param name="includeIgnored"></param>
        /// <param name="supportedMethods"></param>
        /// <param name="extras"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<DevicesResponse> GetDevicesAsync(
            bool includeIgnored = false,
            string supportedMethods = null,
            string extras = null,
            string format = Constraints.JsonFormat);
    }
}