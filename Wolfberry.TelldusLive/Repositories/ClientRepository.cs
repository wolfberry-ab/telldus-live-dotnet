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
    }
}
