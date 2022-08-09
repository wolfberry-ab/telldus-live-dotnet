namespace Wolfberry.TelldusLive.Authentication
{
    public class TelldusOAuth1Configuration
    {
        public string AccessTokenUrl { get; set; }

        public string AuthorizeTokenUrl { get; set; }

        public string RequestTokenUrl { get; set; }

        /// <summary>
        /// Not needed for personal use if access tokens are generated
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Not needed for personal use if access tokens are generated
        /// </summary>
        public string ConsumerKeySecret { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }
    }
}
