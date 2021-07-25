using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Sensor;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface ISensorRepository
    {
        /// <summary>
        /// Get a list of sensors associated with the current user
        /// </summary>
        /// <param name="includeIgnored">Include ignored sensors or not</param>
        /// <param name="includeValues">Include last values for sensor or not</param>
        /// <param name="includeScale">Include scale for values. Require includeValues is true</param>
        /// <param name="includeUnit">Include unit for values. Require includeScale is true</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<SensorsResponse> GetSensorsAsync(
            bool includeIgnored,
            bool includeValues,
            bool? includeScale = null,
            bool? includeUnit = null,
            string format = ResponseFormat.JsonFormat);


        /// <summary>
        /// Get sensor information. Max poll time is once per 10 minutes.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="includeUnit"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<SensorResponse> GetSensorInfoAsync(
            string sensorId, 
            bool includeUnit, 
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Mark a sensor as ignored or not. Ignored sensors are not shown in the sensor list
        /// if not explicitly set to be shown.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="ignore"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> IgnoreAsync(
            string sensorId,
            bool ignore,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Rename a sensor
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="name"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string sensorId,
            string name,
            string format = ResponseFormat.JsonFormat);

        Task<SensorHistoryResponse> GetHistoryAsync(
            string sensorId,
            bool includeKey,
            bool includeUnit,
            bool includeHumanReadableDate,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Clears the sensor history. NOTE: Beta version of Telldus API
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveHistoryAsync(
            string sensorId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Removes a single sensor value from history. NOTE: Beta version of Telldus API
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="timeUuid"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveValueAsync(
            string sensorId,
            string timeUuid,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Reset max/min for a sensor (both temp and humidity will be reset if sensor supports both)
        /// NOTE: Beta version of Telldus API
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="type">"max", "min" or blank to reset both</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> ResetMaxMin(
            string sensorId,
            string type = null,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Enable saving sensor history.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="keepHistory">true to save, false to disable. Disable history will not remove existing data.</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetKeepHistoryAsync(
            string sensorId,
            bool keepHistory,
            string format = ResponseFormat.JsonFormat);
    }
}