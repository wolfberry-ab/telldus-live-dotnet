using System.Globalization;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Client;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <inheritdoc cref="IClientRepository"/>
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        /// <inheritdoc cref="IClientRepository"/>
        public async Task<ClientsResponse> GetClientsAsync(
            string extras = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/clients/list");

            urlBuilder.AddOptionalEscapedQuery("extras", extras);

            var url = urlBuilder.Build();

            return await GetOrThrow<ClientsResponse>(url);
        }

        public async Task<ClientInfoResponse> GetClientInfoAsync(
            string clientId,
            string uuid = null,
            string code = null,
            string extras = null,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/info");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddOptionalQuery("uuid", uuid);
            urlBuilder.AddOptionalQuery("code", code);
            urlBuilder.AddOptionalEscapedQuery("extras", extras);

            var url = urlBuilder.Build();

            return await GetOrThrow<ClientInfoResponse>(url);
        }

        public async Task<StatusResponse> RegisterAsync(
            string clientId,
            string uuid,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/register");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddQuery("uuid", uuid);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveAsync(
            string clientId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/remove");

            urlBuilder.AddQuery("id", clientId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetCoordinatesAsync(string clientId,
                                                        double longitude,
                                                        double latitude,
                                                        string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/setCoordinates");

            urlBuilder.AddQuery("id", clientId);
            // Lat and long in dot (.) notation (not decimal ",")
            urlBuilder.AddQuery("longitude", longitude.ToString(CultureInfo.InvariantCulture));
            urlBuilder.AddQuery("latitude", latitude.ToString(CultureInfo.InvariantCulture));

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetNameAsync(
            string clientId,
            string name,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/setName");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddAsEscapedQuery("name", name);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> EnablePushAsync(
            string clientId,
            bool enablePush,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/setPush");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddQuery("enable", enablePush);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetTimezoneAsync(
            string clientId,
            string timezone,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/setTimezone");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddAsEscapedQuery("timezone", timezone);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> TransferAsync(
            string clientId,
            string email,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/client/transfer");

            urlBuilder.AddQuery("id", clientId);
            urlBuilder.AddAsEscapedQuery("email", email);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }
    }
}
