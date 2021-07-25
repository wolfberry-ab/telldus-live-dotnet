using System.Collections.Generic;

namespace Wolfberry.TelldusLive.Models.Device
{
    public class HistoryResponse
    {
        public List<HistoryEntry> History { get; set; }
        public string Timezone { get; set; }
        public bool TimezoneAutDetected { get; set; }
        public int TzOffset { get; set; }
    }
}
