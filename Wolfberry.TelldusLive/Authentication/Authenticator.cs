using System.Net.Http;
using System.Threading.Tasks;
using TinyOAuth1;

namespace Wolfberry.TelldusLive.Authentication
{
    /// <summary>
    /// Handles the authentication
    /// </summary>
    public interface IAuthenticator
    {
        HttpClient HttpClient { get; set; }

        /// <summary>
        /// Currently not in use
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthorizationUrlAsync();

        void InitializeHttpClient();

        /// <summary>
        /// Currently not in use
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenInfo> FinalizeAuthorizationAsync();
    }

    public class Authenticator : IAuthenticator
    {
		private readonly TinyOAuthConfig _tinyConfig;
		private TinyOAuth _tinyOAuth;
		private RequestTokenInfo _requestTokenInfo;
        private readonly TelldusOAuth1Configuration _telldusConfiguration;
        public HttpClient HttpClient { get; set; }

        public Authenticator(TelldusOAuth1Configuration telldusConfiguration)
        {
            _telldusConfiguration = telldusConfiguration;
			_tinyConfig = new TinyOAuthConfig
			{
				AccessTokenUrl = telldusConfiguration.AccessTokenUrl,
				AuthorizeTokenUrl = telldusConfiguration.AuthorizeTokenUrl,
				RequestTokenUrl = telldusConfiguration.RequestTokenUrl,
				ConsumerKey = telldusConfiguration.ConsumerKey,
				ConsumerSecret = telldusConfiguration.ConsumerKeySecret
			};
        }

		/// <inheritdoc cref="IAuthenticator"/>
		public async Task<string> GetAuthorizationUrlAsync()
		{
            _tinyOAuth = new TinyOAuth(_tinyConfig);
			_requestTokenInfo = await _tinyOAuth.GetRequestTokenAsync();
			var authorizationUrl = _tinyOAuth.GetAuthorizationUrl(_requestTokenInfo.RequestToken);

			return authorizationUrl;
		}

		public void InitializeHttpClient()
		{
			HttpClient = new HttpClient(new TinyOAuthMessageHandler(
                _tinyConfig, 
                _telldusConfiguration.AccessToken, 
                _telldusConfiguration.AccessTokenSecret));
		}

        /// <inheritdoc cref="IAuthenticator"/>
		public async Task<AccessTokenInfo> FinalizeAuthorizationAsync()
		{
			var accessTokenInfo = await _tinyOAuth.GetAccessTokenAsync(_requestTokenInfo.RequestToken, _requestTokenInfo.RequestTokenSecret, "");

			return accessTokenInfo;
		}
	}

}
