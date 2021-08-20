using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.User;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface IUserRepository
    {
        // TODO: SMSHistory

        // TODO: acceptEULA

        // TODO: activateCoupon

        // TODO: changeEmail

        // TODO: changeLocale

        // TODO: changePassword

        // TODO: deletePushToken

        // TODO: eula

        // TODO: linkedAccountAdd

        // TODO: linkedAccountLis

        // TODO: linkedAccountRemove

        /// <summary>
        /// Get registered phones
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<PhonesResponse> GetPhonesAsync(string format = Constraints.JsonFormat);

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<ProfileResponse> GetProfileAsync(string format = Constraints.JsonFormat);

        // TODO: registerPushToken

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
            string format = Constraints.JsonFormat);

        // TODO: setName

        // TODO: unregisterPushToken
    }
}