using System.Collections.Generic;

namespace Wolfberry.TelldusLive.Models.Sensor
{
    /// <summary>
    /// Response from Telldus Live API (with singular name of the list attribute)
    /// </summary>
    public class TelldusSensorsResponse
    {
        public List<Sensor> Sensor { get; set; }
    }

    /// <summary>
    /// Response with sensors
    /// </summary>
    public class SensorsResponse
    {
        public SensorsResponse(List<Sensor> sensors)
        {
            Sensors = sensors;
        }

        public List<Sensor> Sensors { get; set; }
    }
}