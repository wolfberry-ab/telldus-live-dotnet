using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Models.Client;
using Wolfberry.TelldusLive.Repositories;

namespace Wolfberry.TelldusLive
{
    public class TelldusLiveClient
    {
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

        public UserRepository User { get; set; }

        public SensorRepository Sensors { get; set; }

        public SchedulerRepository Scheduler { get; set; }

        public GroupRepository Groups { get; set; }

        public EventRepository Events { get; set; }

        public ClientRepository Clients { get; set; }

        public DeviceRepository Devices { get; set; }
    }
}
