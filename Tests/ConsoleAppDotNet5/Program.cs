using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;

namespace ConsoleAppDotNet5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Get your keys and tokens from https://api.telldus.com/keys/index
            // Public key in Telldus API site
            var consumerKey = "";
            /// Priate key in Telldus API site
            var consumerKeySecret = "";
            var accessToken = "";
            var accessTokenSecret = "";

            ITelldusLiveClient client =
                new TelldusLiveClient(
                    consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

            var clients = await client.Clients.GetClientsAsync();

            Console.WriteLine(JsonConvert.SerializeObject(clients));
        }
    }
}
