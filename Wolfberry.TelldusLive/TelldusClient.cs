using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive
{
    public class TelldusClient
    {
        /// <summary>
        /// The actual call should be performed at: https://api.telldus.com/{format}/{function}
        ///  Where:
        ///  {format} is the returned format and should be either json or xml
        ///  {function} is the function to call.
        /// </summary>
        public string BaseUrl { get; } = "http://api.telldus.com";
        private readonly Authenticator _authenticator;

        public TelldusClient(Authenticator authenticator)
        {
            _authenticator = authenticator;
            _authenticator.InitializeHttpClient();
        }

        private async Task<string> GetAsJsonStringAsync(string uri)
        {
            var response = await _authenticator.HttpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetResponseAsType<T>(string url)
        {
            var responseJson = await GetAsJsonStringAsync(url);
            var response = JsonUtil.Deserialize<T>(responseJson);
            return response;
        }
    }
}
