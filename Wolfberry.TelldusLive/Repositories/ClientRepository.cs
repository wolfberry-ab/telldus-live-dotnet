using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public class ClientRepository
    {
        private readonly TelldusClient _client;

        public ClientRepository(TelldusClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Returns a list of clients owned by the current user (e.g. of type "TellStick ZNet Lite v2").
        /// </summary>
        /// <param name="extras">(optional) A comma-delimited list of extra information to fetch for each
        /// returned client. Currently supported fields are: coordinate, features, insurance,
        /// latestversion, suntime, timezone, transports and tzoffset</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        public async Task<ClientsResponse> GetClientsAsync(string extras, string format = Constraints.JsonFormat)
        {

            var requestUri = $"{_client.BaseUrl}/{format}/clients/list";

            if (extras != null)
            {
                requestUri += $"?extras={extras}";
            }

            var response = await _client.GetResponseAsType<ClientsResponse>(requestUri);

            return response;
        }
    }
}
