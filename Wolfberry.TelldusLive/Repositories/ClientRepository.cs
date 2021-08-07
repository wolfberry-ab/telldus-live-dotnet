using System;
using System.Globalization;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IClientRepository
    {
        /// <summary>
        /// Returns a list of clients owned by the current user (e.g. of type "TellStick ZNet Lite v2").
        /// </summary>
        /// <param name="extras">(optional) A comma-delimited list of extra information to fetch for each
        /// returned client. Currently supported fields are: coordinate, features, insurance,
        /// latestversion, suntime, timezone, transports and tzoffset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ClientsResponse> GetClientsAsync(string extras, string format = Constraints.JsonFormat);

        /// <summary>
        /// Get client information
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="uuid">Optional</param>
        /// <param name="code">Optional</param>
        /// <param name="extras">Optional</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ClientInfoResponse> GetClientInfo(
            string clientId,
            string uuid = null,
            string code = null,
            string extras = null,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Register an unregistered client to the calling user. Not yet implemented.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        Task<object> Register(string clientId, string uuid);

        /// <summary>
        /// Removes a client from the user. The client needs to be activated again in order to be used.
        /// Not yet implemented.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<object> Remove(string clientId);

        /// <summary>
        /// Sets the coordinates where the client is located.
        /// This can be used for calculating for example sunset and sunrise times.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> SetCoordinates(
            string clientId, 
            double longitude, 
            double latitude,
            string format = Constraints.JsonFormat);

        /// <summary>
        /// Rename client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<StatusResponse> SetName(
            string clientId, 
            string name,
            string format = Constraints.JsonFormat);

        Task<StatusResponse> EnablePush(
            string clientId, 
            bool enablePush,
            string format = Constraints.JsonFormat);

        Task<StatusResponse> SetTimezone(
            string clientId, 
            string timezone,
            string format = Constraints.JsonFormat);

        Task<StatusResponse> Transfer(
            string clientId,
            string email,
            string format = Constraints.JsonFormat);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public ClientRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="IClientRepository"/>
        public async Task<ClientsResponse> GetClientsAsync(string extras, string format = Constraints.JsonFormat)
        {

            var requestUri = $"{_httpClient.BaseUrl}/{format}/clients/list";

            if (extras != null)
            {
                requestUri += $"?extras={extras}";
            }

            var response = await _httpClient.GetResponseAsType<ClientsResponse>(requestUri);

            return response;
        }

        public async Task<ClientInfoResponse> GetClientInfo(string clientId, 
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

        public async Task<object> Register(string clientId, string uuid)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> Remove(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<StatusResponse> SetCoordinates(string clientId, 
                                                        double longitude, 
                                                        double latitude, 
                                                        string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setCoordinates?id={clientId}";
            // Lat and long in dot (.) notation (not decimal ",")
            requestUri += $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}";
            requestUri += $"&latitude={latitude.ToString(CultureInfo.InvariantCulture)}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> SetName(
            string clientId, 
            string name,
            string format = Constraints.JsonFormat)
        {
            var encodedName = Uri.EscapeDataString(name);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setName?id={clientId}&name={encodedName}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> EnablePush(
            string clientId, 
            bool enablePush, 
            string format = Constraints.JsonFormat)
        {
            var enableInteger = enablePush ? 1 : 0;
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setPush?id={clientId}&enable={enableInteger}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> SetTimezone(
            string clientId, 
            string timezone, 
            string format = Constraints.JsonFormat)
        {
            var encodedTimezone = Uri.EscapeDataString(timezone);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/client/setTimezone?id={clientId}&timezone={encodedTimezone}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> Transfer(
            string clientId,
            string email,
            string format = Constraints.JsonFormat)
        {
            throw new NotImplementedException();
        }
    }
}
