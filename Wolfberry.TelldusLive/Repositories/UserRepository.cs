﻿using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.User;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
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

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
