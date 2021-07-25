using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public GroupRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        public async Task<StatusResponse> AddGroupAsync(
            string clientId,
            string name,
            string devices,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/group/add");

            urlBuilder.AddQuery("clientId", clientId);
            urlBuilder.AddAsEscapedQuery("name", name);
            urlBuilder.AddOptionalQuery("devices", devices);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveGroupAsync(
            string groupId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/group/remove");

            urlBuilder.AddQuery("id", groupId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }
    }
}
