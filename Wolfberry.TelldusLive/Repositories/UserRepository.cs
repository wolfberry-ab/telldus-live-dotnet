using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.ViewModels;
using Wolfberry.TelldusLive.ViewModels.User;

namespace Wolfberry.TelldusLive.Repositories
{
    public class UserRepository
    {
        private readonly TelldusClient _client;

        public UserRepository(TelldusClient client)
        {
            _client = client;
        }

        public async Task<PhonesResponse> GetPhonesAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/user/listPhones";

            var response = await _client.GetResponseAsType<PhonesResponse>(requestUri);

            return response;
        }

        public async Task<ProfileResponse> GetProfileAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_client.BaseUrl}/{format}/user/profile";

            var response = await _client.GetResponseAsType<ProfileResponse>(requestUri);

            return response;
        }

        /// <summary>
        /// Send push message to device
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="message">Special characters will automatically be URI escaped</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public async Task<StatusResponse> SendPushTest(string phoneId, string message, string format = Constraints.JsonFormat)
        {
            var encodedMessage = Uri.EscapeDataString(message);
            var requestUri = $"{_client.BaseUrl}/{format}/user/sendPushTest?phoneId={phoneId}&message={encodedMessage}";

            var response = await _client.GetResponseAsType<StatusResponse>(requestUri);

            return response;
        }
    }
}
