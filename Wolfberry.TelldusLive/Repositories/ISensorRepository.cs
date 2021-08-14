using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;

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

        // TODO: info

        // TODO: setIgnore

        // TODO: setNAme

        // TODO: history

        // TODO: removeHistory

        // TODO: removeValue

        // TODO: resetMaxMin

        // TODO: setKeepHistory
    }
}