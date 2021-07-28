using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Repositories;
using Wolfberry.TelldusLive.ViewModels;

namespace ApiMonitor
{
    public static class TelldusApiRouter
    {
        public static async Task<IActionResult> Dispatch(TelldusClient client, string resource, string resourceParameter)
        {
            UserRepository userRepository;
            StatusResponse status;
            DeviceRepository deviceRepository;
            switch (resource)
            {
                case "clients":
                    var clientRepository = new ClientRepository(client);
                    var clients = await clientRepository.GetClientsAsync(resourceParameter);
                    return new OkObjectResult(clients);
                case "sensors":
                    var sensorRepository = new SensorRepository(client);
                    var sensors = await sensorRepository.GetSensorsAsync();
                    return new OkObjectResult(sensors);
                case "events":
                    var eventRepository = new EventRepository(client);
                    var events = await eventRepository.GetEventsAsync(resourceParameter);
                    return new OkObjectResult(events);
                case "user/phones":
                    userRepository = new UserRepository(client);
                    var phones = await userRepository.GetPhonesAsync();
                    return new OkObjectResult(phones);
                case "user/profile":
                    userRepository = new UserRepository(client);
                    var profile = await userRepository.GetProfileAsync();
                    return new OkObjectResult(profile);
                case "user/pushTest":
                    userRepository = new UserRepository(client);
                    var phoneId = resourceParameter.Split(",")[0];
                    var message = resourceParameter.Split(",")[1];
                    status = await userRepository.SendPushTest(phoneId, message);
                    return new OkObjectResult(status);
                case "device/on":
                    deviceRepository = new DeviceRepository(client);
                    status = await deviceRepository.TurnOnAsync(resourceParameter);
                    return new OkObjectResult(status);
                case "device/off":
                    deviceRepository = new DeviceRepository(client);
                    status = await deviceRepository.TurnOffAsync(resourceParameter);
                    return new OkObjectResult(status);
                default:
                    return new OkObjectResult("No resource specified");
            }
        }
    }
}
