namespace Wolfberry.TelldusLive.Models.Client
{
    public class ClientInfoResponse
    {
        public string Id { get; set; }
        public string Ip { get; set; }
        /// <summary>
        /// According to https://datatracker.ietf.org/doc/html/rfc4122
        /// </summary>
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Online { get; set; }
        public int Editable { get; set; }
        public int Extensions { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }

        // TODO: Check if missing attributes when using extras parameter
    }
}
