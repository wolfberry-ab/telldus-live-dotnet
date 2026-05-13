using System;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Configuration;
using Wolfberry.TelldusLive.Repositories;

namespace Wolfberry.TelldusLive
{
    public interface ITelldusLiveClient : IDisposable
    {
        IUserRepository User { get; }
        ISensorRepository Sensors { get; }
        ISchedulerRepository Scheduler { get; }
        IGroupRepository Groups { get; }
        IEventRepository Events { get; }
        IClientRepository Clients { get; }
        IDeviceRepository Devices { get; }
    }

    /// <summary>
    /// Telldus Live client.
    /// </summary>
    public class TelldusLiveClient : ITelldusLiveClient
    {
        private readonly ITelldusHttpClient _httpClient;

        /// <summary>
        /// Create a Telldus Live client
        /// </summary>
        /// <param name="consumerKey">The Public Key in the API portal</param>
        /// <param name="consumerKeySecret">The Private Key in the API portal</param>
        /// <param name="accessToken">Named Token in the API portal</param>
        /// <param name="accessTokenSecret">Named Token Secret in the API portal</param>
        /// <param name="customBaseUrl">Optional/custom API URL, set to null/empty to use default</param>
        public TelldusLiveClient(
            string consumerKey,
            string consumerKeySecret,
            string accessToken,
            string accessTokenSecret,
            string customBaseUrl = null)
        {
            var baseUrl = "https://api.telldus.com";
            if (!string.IsNullOrEmpty(customBaseUrl))
            {
                baseUrl = customBaseUrl.TrimEnd();
            }

            var config = new TelldusOAuth1Configuration
            {
                AccessTokenUrl = $"{baseUrl}/oauth/accessToken",
                AuthorizeTokenUrl = $"{baseUrl}/oauth/authorize",
                RequestTokenUrl = $"{baseUrl}/oauth/requestToken",
                ConsumerKey = consumerKey,
                ConsumerKeySecret = consumerKeySecret,
                AccessToken = accessToken,
                AccessTokenSecret = accessTokenSecret
            };

            ValidateConfiguration(config);

            var authenticator = new Authenticator(config);
            var client = new TelldusHttpClient(authenticator, baseUrl);
            _httpClient = client;

            Clients = new ClientRepository(client);
            Devices = new DeviceRepository(client);
            Events = new EventRepository(client);
            Groups = new GroupRepository(client);
            Scheduler = new SchedulerRepository(client);
            Sensors = new SensorRepository(client);
            User = new UserRepository(client);
        }

        private static void ValidateConfiguration(TelldusOAuth1Configuration config)
        {
            if (string.IsNullOrEmpty(config.ConsumerKey))
            {
                throw new ConfigurationException("No ConsumerKey found in configuration");
            }

            if (string.IsNullOrEmpty(config.ConsumerKeySecret))
            {
                throw new ConfigurationException("No ConsumerKeySecret found in configuration");
            }

            if (string.IsNullOrEmpty(config.AccessToken))
            {
                throw new ConfigurationException("No AccessToken found in configuration");
            }

            if (string.IsNullOrEmpty(config.AccessTokenSecret))
            {
                throw new ConfigurationException("No AccessToken found in configuration");
            }
        }

        public IUserRepository User { get; }

        public ISensorRepository Sensors { get; }

        public ISchedulerRepository Scheduler { get; }

        public IGroupRepository Groups { get; }

        public IEventRepository Events { get; }

        public IClientRepository Clients { get; }

        public IDeviceRepository Devices { get; }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
