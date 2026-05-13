using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;

namespace ConsoleAppDotNet8
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
            // Optional/custom API URL, set to null/empty to use default
            var customApiBaseUrl = "https://api.telldus.com";

            ITelldusLiveClient client =
                new TelldusLiveClient(
                    consumerKey, consumerKeySecret, accessToken, accessTokenSecret, customApiBaseUrl);

            var clients = await client.Clients.GetClientsAsync();
            Console.WriteLine(JsonConvert.SerializeObject(clients));

            var sensors = await client.Sensors.GetSensorsAsync(false, true);
            if (sensors.Sensors.Count == 0)
            {
                Console.WriteLine("No sensors found");
                return;
            }

            var firstSensor = sensors.Sensors.First();
            var yesterdayTimestamp = (int)DateTimeOffset.Now.AddDays(-1).ToUnixTimeSeconds();
            var todayTimestamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            var sensorHistory = await client.Sensors.GetHistoryAsync(
                firstSensor.Id,
                true,
                true,
                true,
                yesterdayTimestamp,
                todayTimestamp);

            Console.WriteLine(JsonConvert.SerializeObject(sensorHistory, Formatting.Indented));
        }
    }
}
