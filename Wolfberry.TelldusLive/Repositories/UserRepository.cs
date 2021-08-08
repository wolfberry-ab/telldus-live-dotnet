using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;
using Wolfberry.TelldusLive.ViewModels.User;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get registered phones
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<PhonesResponse> GetPhonesAsync(string format = Constraints.JsonFormat);

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<ProfileResponse> GetProfileAsync(string format = Constraints.JsonFormat);

        /// <summary>
        /// Send push message to device
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="message">Special characters will automatically be URI escaped</param>
        /// <param name="format"></param>
        /// <returns></returns>
        Task<StatusResponse> SendPushTestAsync(
            string phoneId,
            string message,
            string format = Constraints.JsonFormat);
    }

    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public UserRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PhonesResponse> GetPhonesAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/listPhones";

            var response = await _httpClient.GetResponseAsType<PhonesResponse>(requestUri);

            return response;
        }

        public async Task<ProfileResponse> GetProfileAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/profile";

            var response = await _httpClient.GetResponseAsType<ProfileResponse>(requestUri);

            return response;
        }

        public async Task<StatusResponse> SendPushTestAsync(
            string phoneId, 
            string message, 
            string format = Constraints.JsonFormat)
        {
            var encodedMessage = Uri.EscapeDataString(message);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/sendPushTest?phoneId={phoneId}&message={encodedMessage}";

            var response = await _httpClient.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }
    }
}
