using System;
using System.Collections.Generic;
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

        public async Task<List<SmsHistoryEntry>> GetSmsHistoryAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/SMSHistory";

            return await GetOrThrow<List<SmsHistoryEntry>>(requestUri);
        }

        public async Task<StatusResponse> AcceptEulaAsync(
            int version,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/acceptEULA?version={version}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> ActivateCouponAsync(
            string code,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/activateCoupon?code={code}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> ChangeEmailAsync(
            string newEmail,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/changeEmail?email={newEmail}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> ChangeLocaleAsync(
            string locale,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/changeLocale?locale={locale}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> ChangePasswordAsync(
            string currentPassword,
            string newPassword,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/changeLocale?currentPassword={currentPassword}";

            requestUri += $"&newPassword={newPassword}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> DeletePushTokenAsync(
            string token,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/deletePushToken?token={token}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<EulaResponse> GetEulaAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/eula";

            return await GetOrThrow<EulaResponse>(requestUri);
        }

        public async Task<StatusResponse> AddLinkedAccountAsync(
            string provider,
            string idToken,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/linkedAccountAdd?provider={provider}";

            requestUri += $"&idToken={idToken}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<object> GetLinkedAccountsAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/linkedAccountList";

            return await GetOrThrow<object>(requestUri);
        }

        public async Task<StatusResponse> RemoveLinkedAccountAsync(
            string provider,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/linkedAccountRemove?provider={provider}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<PhonesResponse> GetPhonesAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/listPhones";

            return await GetOrThrow<PhonesResponse>(requestUri);
        }

        public async Task<ProfileResponse> GetProfileAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/profile";

            return await GetOrThrow<ProfileResponse>(requestUri);
        }

        public async Task<StatusResponse> RegisterPushTokenAsync(
            string token,
            string name,
            string model,
            string manufacturer,
            string osVersion,
            string deviceId,
            string pushServiceId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/registerPushToken?token={token}";

            requestUri += $"&name={name}&model={model}&manufacturer={manufacturer}&osVersion={osVersion}";
            requestUri += $"&deviceId={deviceId}&pushServiceId={pushServiceId}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SendPushTestAsync(
            string phoneId, 
            string message, 
            string format = Constraints.JsonFormat)
        {
            var escapedMessage = Uri.EscapeDataString(message);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/sendPushTest?phoneId={phoneId}&message={escapedMessage}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> SetNameAsync(
            string firstName,
            string lastName,
            string format = Constraints.JsonFormat)
        {
            var escapedFirstName = Uri.EscapeDataString(firstName);
            var escapedLastName = Uri.EscapeDataString(lastName);
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/setName?firstname={escapedFirstName}&lastname={escapedLastName}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }

        public async Task<StatusResponse> UnregisterPushToken(
            string token,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/user/unregisterPushToken?token={token}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
