namespace Wolfberry.TelldusLive.ViewModels
{
    public class Trigger
    {
        public string Id { get; set; }

        /// <summary>
        /// E.g. "sensor" or "device"
        /// </summary>
        public string Type { get; set; }
        public string ClientId { get; set; }
    }
}
