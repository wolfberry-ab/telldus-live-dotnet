namespace Wolfberry.TelldusLive.Models.Sensor
{
    public class SensorData
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Scale { get; set; }
        public int LastUpdated { get; set; }
        public string Max { get; set; }
        public string MaxTime { get; set; }
        public string Min { get; set; }
        public string MinTime { get; set; }
        public string Unit { get; set; }
    }
}
