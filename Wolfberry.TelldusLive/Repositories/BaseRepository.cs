using System.Threading.Tasks;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public class BaseRepository
    {
        protected readonly ITelldusHttpClient _httpClient;

        public BaseRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<T> GetOrThrow<T>(string url)
        {
            var responseJson = await _httpClient.GetAsJsonAsync(url);

            var errorMessage = ErrorParser.GetOrCreateErrorMessage(responseJson);
            if (errorMessage != null)
            {
                var exception = new RepositoryException(errorMessage);
                exception.Data.Add("Url", url);
                throw exception;
            }

            var response = JsonUtil.Deserialize<T>(responseJson);
            return response;
        }
    }
}
