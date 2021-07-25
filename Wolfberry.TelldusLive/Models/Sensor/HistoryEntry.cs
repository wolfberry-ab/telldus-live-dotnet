using System.Collections.Generic;

namespace Wolfberry.TelldusLive.Models.Sensor
{
    public class HistoryEntry
    {
        public int Ts { get; set; }
        public string Uuid { get; set; }
        public string Date { get; set; }
        public List<HistoryData> Data { get; set; }
    }
}
