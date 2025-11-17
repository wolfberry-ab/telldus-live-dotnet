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
            var consumerKey = "FEHUVEW84RAFR5SP22RABURUPHAFRUNU";
            /// Priate key in Telldus API site
            var consumerKeySecret = "ZUXEVEGA9USTAZEWRETHAQUBUR69U6EF";
            var accessToken = "66731a835637f7ea1cbaab7a34be4fa305db0b53e";
            var accessTokenSecret = "01008b2537a3e30b99998e927f488565";
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
