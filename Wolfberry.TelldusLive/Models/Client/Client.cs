namespace Wolfberry.TelldusLive.Models.Client
{
    public class Client
    {
        public string Id { get; set; }
        /// <summary>
        /// According to https://datatracker.ietf.org/doc/html/rfc4122
        /// </summary>
        public string Uuid { get; set; }
        public string Name { get; set; }
        public int Online { get; set; }
        public int Editable { get; set; }
        public int Extensions { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string Ip { get; set; }
        public double Longitute { get; set; }
        public double Latitute { get; set; }
        public string Features { get; set; }
        public bool Insured { get; set; }
        public string LatestVersion { get; set; }
        public string DownloadUrl { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public string Timezone { get; set; }
        public bool TimezoneAutodetected { get; set; }
        public string Transports { get; set; }
        public int TzOffset { get; set; }
    }
}
