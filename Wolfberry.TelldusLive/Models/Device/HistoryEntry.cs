namespace Wolfberry.TelldusLive.Models.Device
{
    public class HistoryEntry
    {
        public int Ts { get; set; }
        public int State { get; set; }
        public int StateValue { get; set; }

        /// <summary>
        /// E.g. "Incoming signal"
        /// </summary>
        public string Origin { get; set; }
        public int SuccessStatus { get; set; }
    }
}
