using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.User;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        public async Task<SmsHistoryEntryResponse> GetSmsHistoryAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/SMSHistory");

            var url = urlBuilder.Build();

            var history = await GetOrThrow<List<SmsHistoryEntry>>(url);

            // Create a better response type
            return new SmsHistoryEntryResponse
            {
                History = history
            };
        }

        public async Task<StatusResponse> AcceptEulaAsync(
            int version,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/acceptEULA");

            urlBuilder.AddQuery("version", version);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> ActivateCouponAsync(
            string code,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/activateCoupon");

            urlBuilder.AddQuery("code", code);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> ChangeEmailAsync(
            string newEmail,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/changeEmail");

            urlBuilder.AddAsEscapedQuery("email", newEmail);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> ChangeLocaleAsync(
            string locale,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/changeLocale");

            urlBuilder.AddQuery("locale", locale);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> ChangePasswordAsync(
            string currentPassword,
            string newPassword,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/changeLocale");

            urlBuilder.AddAsEscapedQuery("currentPassword", currentPassword);
            urlBuilder.AddAsEscapedQuery("newPassword", newPassword);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> DeletePushTokenAsync(
            string token,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/deletePushToken");

            urlBuilder.AddAsEscapedQuery("token", token);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<EulaResponse> GetEulaAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/eula");

            var url = urlBuilder.Build();

            return await GetOrThrow<EulaResponse>(url);
        }

        public async Task<StatusResponse> AddLinkedAccountAsync(
            string provider,
            string idToken,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/linkedAccountAdd");

            urlBuilder.AddQuery("provider", provider);
            urlBuilder.AddQuery("idToken", idToken);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<object> GetLinkedAccountsAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/linkedAccountList");

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> RemoveLinkedAccountAsync(
            string provider,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/linkedAccountRemove");

            urlBuilder.AddQuery("provider", provider);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<PhonesResponse> GetPhonesAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/listPhones");

            var url = urlBuilder.Build();

            return await GetOrThrow<PhonesResponse>(url);
        }

        public async Task<ProfileResponse> GetProfileAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/profile");

            var url = urlBuilder.Build();

            return await GetOrThrow<ProfileResponse>(url);
        }

        public async Task<StatusResponse> RegisterPushTokenAsync(
            string token,
            string name,
            string model,
            string manufacturer,
            string osVersion,
            string deviceId,
            string pushServiceId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/registerPushToken");

            urlBuilder.AddQuery("token", token);
            urlBuilder.AddAsEscapedQuery("name", name);
            urlBuilder.AddQuery("model", model);
            urlBuilder.AddQuery("manufacturer", manufacturer);
            urlBuilder.AddQuery("osVersion", osVersion);
            urlBuilder.AddQuery("deviceId", deviceId);
            urlBuilder.AddQuery("pushServiceId", pushServiceId);
            urlBuilder.AddQuery("token", token);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SendPushTestAsync(
            string phoneId, 
            string message, 
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/sendPushTest");

            urlBuilder.AddQuery("phoneId", phoneId);
            // TODO: Check correct format of message (%21)
            urlBuilder.AddAsEscapedQuery("message", message);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetNameAsync(
            string firstName,
            string lastName,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/setName");

            urlBuilder.AddAsEscapedQuery("firstname", firstName);
            urlBuilder.AddAsEscapedQuery("lastname", lastName);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> UnregisterPushToken(
            string token,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/user/unregisterPushToken");

            urlBuilder.AddAsEscapedQuery("token", token);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }
    }
}
