﻿using System.Threading.Tasks;
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
        public string BaseUrl { get; } = "http://api.telldus.com";
        private readonly IAuthenticator _authenticator;

        public TelldusHttpClient(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _authenticator.InitializeHttpClient();
        }

        public async Task<string> GetAsJsonAsync(string uri)
        {
            var response = await _authenticator.HttpClient.GetAsync(uri);
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
