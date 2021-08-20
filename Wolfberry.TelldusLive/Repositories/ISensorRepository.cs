using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Sensor;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface ISensorRepository
    {
        /// <summary>
        /// Returns a list of all sensors associated with the current user
        /// https://api.telldus.net/explore/sensors/list
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<IList<Sensor>> GetSensorsAsync(string format = Constraints.JsonFormat);


        /// <summary>
        /// Get sensor information. Max poll time is once per 10 minutes.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="includeUnit"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<SensorResponse> GetSensorInfoAsync(
            string sensorId, 
            bool includeUnit, 
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Mark a sensor as ignored or not. Ignored sensors are not shown in the sensor list
        /// if not explicitly set to be shown.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="ignore"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> IgnoreAsync(
            string sensorId,
            bool ignore,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Rename a sensor
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string sensorId,
            string name,
            string format = Constraints.JsonFormat);

        Task<SensorHistoryResponse> GetHistoryAsync(
            string sensorId,
            bool includeKey,
            bool includeUnit,
            bool includeHumanReadableDate,
            int? fromTimestamp = null,
            int? toTimestamp = null,
            string format = Constraints.JsonFormat);

        // TODO: removeHistory

        // TODO: removeValue

        // TODO: resetMaxMin

        // TODO: setKeepHistory
    }
}