namespace Wolfberry.TelldusLive.ViewModels.User
{
    public class ProfileResponse
    {
        /// <summary>
        /// According to https://datatracker.ietf.org/doc/html/rfc4122
        /// </summary>
        public string Uuid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Credits { get; set; }
        public int Pro { get; set; }
        public string Subscription { get; set; }

        /// <summary>
        /// E.g. "auto"
        /// </summary>
        public string Locale { get; set; }
        public int Admin { get; set; }
        public int Eula { get; set; }
        public int Permission { get; set; }
    }
}
