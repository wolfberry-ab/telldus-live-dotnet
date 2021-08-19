using System;
using System.Globalization;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Client;

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
            string format = Constraints.JsonFormat)
        {

            var requestUri = $"{_httpClient.BaseUrl}/{format}/clients/list";

            if (extras != null)
            {
                requestUri += $"?extras={extras}";
            }

            var response = await _httpClient.GetResponseAsType<ClientsResponse>(requestUri);

            return response;
        }

        public async Task<ClientInfoResponse> GetClientInfoAsync(string clientId, 
            string uuid = null, 
            string code = null, 
            string extras = null, 
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/info?id={clientId}";
            
            if (uuid != null)
            {
                requestUri += $"&uuid={uuid}";
            }

            if (extras != null)
            {
                requestUri += $"&extras={extras}";
            }

            if (code != null)
            {
                requestUri += $"&code={code}";
            }

            var response = await _httpClient.GetResponseAsType<ClientInfoResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> RegisterAsync(
            string clientId, 
            string uuid,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/register?id={clientId}&uuid={uuid}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveAsync(
            string clientId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/remove?id={clientId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetCoordinatesAsync(string clientId, 
                                                        double longitude, 
                                                        double latitude, 
                                                        string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setCoordinates?id={clientId}";
            // Lat and long in dot (.) notation (not decimal ",")
            requestUri += $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}";
            requestUri += $"&latitude={latitude.ToString(CultureInfo.InvariantCulture)}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetNameAsync(
            string clientId, 
            string name,
            string format = Constraints.JsonFormat)
        {
            var encodedName = Uri.EscapeDataString(name);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setName?id={clientId}&name={encodedName}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> EnablePushAsync(
            string clientId, 
            bool enablePush, 
            string format = Constraints.JsonFormat)
        {
            var enableInteger = enablePush ? 1 : 0;
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setPush?id={clientId}&enable={enableInteger}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetTimezoneAsync(
            string clientId, 
            string timezone, 
            string format = Constraints.JsonFormat)
        {
            var encodedTimezone = Uri.EscapeDataString(timezone);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setTimezone?id={clientId}&timezone={encodedTimezone}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> TransferAsync(
            string clientId,
            string email,
            string format = Constraints.JsonFormat)
        {
            var encodedEmail = Uri.EscapeDataString(email);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/transfer?id={clientId}&email={encodedEmail}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
