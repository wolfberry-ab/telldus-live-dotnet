using System.Threading.Tasks;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive
{
    /// <summary>
    /// Handles HTTP calls
    /// </summary>
    public interface ITelldusHttpClient
    {
        /// <summary>
        /// The actual call should be performed at: https://api.telldus.com/{format}/{function}
        ///  Where:
        ///  {format} is the returned format and should be either json or xml
        ///  {function} is the function to call.
        /// </summary>
        string BaseUrl { get; }

        Task<T> GetResponseAsType<T>(string url);

        Task<string> GetAsJsonAsync(string uri);
    }

    /// <inheritdoc cref="ITelldusHttpClient"/>
    public class TelldusHttpClient : ITelldusHttpClient
    {
        /// <inheritdoc cref="ITelldusHttpClient"/>
        public string BaseUrl { get; }
        private readonly IAuthenticator _authenticator;

        public TelldusHttpClient(IAuthenticator authenticator, string baseUrl)
        {
            _authenticator = authenticator;
            _authenticator.InitializeHttpClient();
            BaseUrl = baseUrl;
        }

        public async Task<string> GetAsJsonAsync(string uri)
        {
            var response = await _authenticator.HttpClient.GetAsync(uri);
            // The status code is not used in Telldus Live API, both OK and error
            // responses are sent as JSON in the response body
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<T> GetResponseAsType<T>(string url)
        {
            var responseJson = await GetAsJsonAsync(url);
            var response = JsonUtil.Deserialize<T>(responseJson);
            return response;
        }
    }
}
