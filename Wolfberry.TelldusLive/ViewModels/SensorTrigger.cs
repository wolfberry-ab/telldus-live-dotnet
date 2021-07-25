namespace Wolfberry.TelldusLive.ViewModels
{
    public class SensorTrigger : Trigger
    {
        public string SensorId { get; set; }
        public string Value { get; set; }
        public string Edge { get; set; }
        
        /// <summary>
        /// E.g. "temp"
        /// </summary>
        public string ValueType { get; set; }
        
        /// <summary>
        /// E.g. "1"
        /// </summary>
        public string ReloadValue { get; set; }

        /// <summary>
        /// E.g. "0"
        /// </summary>
        public string Scale { get; set; }
    }
}
