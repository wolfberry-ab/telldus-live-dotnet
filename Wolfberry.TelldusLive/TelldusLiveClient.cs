using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Configuration;
using Wolfberry.TelldusLive.Repositories;

namespace Wolfberry.TelldusLive
{
    public interface ITelldusLiveClient
    {
        UserRepository User { get; set; }
        SensorRepository Sensors { get; set; }
        SchedulerRepository Scheduler { get; set; }
        GroupRepository Groups { get; set; }
        EventRepository Events { get; set; }
        ClientRepository Clients { get; set; }
        DeviceRepository Devices { get; set; }
    }

    /// <summary>
    /// Telldus Live client.
    /// </summary>
    public class TelldusLiveClient : ITelldusLiveClient
    {
        /// <summary>
        /// Create a Telldus Live client
        /// </summary>
        /// <param name="consumerKey">The Public Key in the API portal</param>
        /// <param name="consumerKeySecret">The Private Key in the API portal</param>
        /// <param name="accessToken">Named Token in the API portal</param>
        /// <param name="accessTokenSecret">Named Token Secret in the API portal</param>
        public TelldusLiveClient(
            string consumerKey,
            string consumerKeySecret,
            string accessToken,
            string accessTokenSecret)
        {
            var config = new TelldusOAuth1Configuration
            {
                AccessTokenUrl = "https://api.telldus.com/oauth/accessToken",
                AuthorizeTokenUrl = "https://api.telldus.com/oauth/authorize",
                RequestTokenUrl = "https://api.telldus.com/oauth/requestToken",
                ConsumerKey = consumerKey,
                ConsumerKeySecret = consumerKeySecret,
                AccessToken = accessToken,
                AccessTokenSecret = accessTokenSecret
            };

            ValidateConfiguration(config);

            var authenticator = new Authenticator(config);
            var client = new TelldusHttpClient(authenticator);

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

        public UserRepository User { get; set; }

        public SensorRepository Sensors { get; set; }

        public SchedulerRepository Scheduler { get; set; }

        public GroupRepository Groups { get; set; }

        public EventRepository Events { get; set; }

        public ClientRepository Clients { get; set; }

        public DeviceRepository Devices { get; set; }
    }
}
