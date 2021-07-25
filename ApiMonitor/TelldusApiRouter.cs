using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wolfberry.TelldusLive;

namespace ApiMonitor
{
    public static class TelldusApiRouter
    {
        public static async Task<IActionResult> Dispatch(TelldusClient client, string resource, string resourceParameter)
        {
            switch (resource)
            {
                case "clients":
                    var clients = await client.GetClientsAsync(resourceParameter);
                    return new OkObjectResult(clients);
                case "sensors":
                    var sensors = await client.GetSensorsAsync();
                    return new OkObjectResult(sensors);
                case "events":
                    var events = await client.GetEventsAsync(resourceParameter);
                    return new OkObjectResult(events);
                default:
                    return new OkObjectResult("No resource specified");
            }
        }
    }
}
