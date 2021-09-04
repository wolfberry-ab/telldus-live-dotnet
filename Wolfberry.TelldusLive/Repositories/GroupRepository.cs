using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;

namespace Wolfberry.TelldusLive.Repositories
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        // TODO: Use UrlBuilder for all requests

        public GroupRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        public async Task<StatusResponse> AddGroupAsync(
            string clientId, 
            string name, 
            string devices, 
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/group/add?clientId={clientId}";
            requestUri += $"&name={name}&devices={devices}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/group/remove?id={groupId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
