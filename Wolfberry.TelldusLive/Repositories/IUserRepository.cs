using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.User;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IUserRepository
    {
        Task<SmsHistoryEntryResponse> GetSmsHistoryAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Accept EULA version. Only latest version can be accepted.
        /// </summary>
        /// <param name="version">E.g 2</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> AcceptEulaAsync(
            int version,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Activate a coupon with code
        /// </summary>
        /// <param name="code">Coupon code</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> ActivateCouponAsync(
            string code,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Change e-mail address. Requires e-mail confirmation.
        /// </summary>
        /// <param name="newEmail">New e-mail to use</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> ChangeEmailAsync(
            string newEmail,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Change locale.
        /// </summary>
        /// <param name="locale">"auto" or ISO value</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> ChangeLocaleAsync(
            string locale,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Change password. Must be called over HTTPS.
        /// </summary>
        /// <param name="currentPassword">Current password</param>
        /// <param name="newPassword">New password</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> ChangePasswordAsync(
            string currentPassword,
            string newPassword,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove push connection to mobile device (also see the un-registration
        /// method which just disables push messages).
        /// </summary>
        /// <param name="token">Registered token id</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> DeletePushTokenAsync(
            string token,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get EULA
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<EulaResponse> GetEulaAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Add linked account. NOTE: Untested method due to unknown response structure
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="idToken"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> AddLinkedAccountAsync(
            string provider,
            string idToken,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Not implemented due to unknown response structure.
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<object> GetLinkedAccountsAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove linked account
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveLinkedAccountAsync(
            string provider,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get registered phones
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<PhonesResponse> GetPhonesAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ProfileResponse> GetProfileAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Register a mobile device to the system so it can receive push notifications
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name">The name of the mobile device</param>
        /// <param name="model">Mobile device model</param>
        /// <param name="manufacturer">Mobile device manufacturer</param>
        /// <param name="osVersion">OS version</param>
        /// <param name="deviceId">Mobile device ID</param>
        /// <param name="pushServiceId">push_service_id</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RegisterPushTokenAsync(
            string token,
            string name,
            string model,
            string manufacturer,
            string osVersion,
            string deviceId,
            string pushServiceId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Send push message to device
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="message">Special characters will automatically be URI escaped</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SendPushTestAsync(
            string phoneId,
            string message,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Set first and last name of user
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetNameAsync(
            string firstName,
            string lastName,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Unregister push token (disable pushing to the mobile device)
        /// </summary>
        /// <param name="token">ID used for pushing notifcations</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> UnregisterPushToken(
            string token,
            string format = ResponseFormat.JsonFormat);
    }
}