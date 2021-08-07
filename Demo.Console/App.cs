using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Repositories;
using Wolfberry.TelldusLive.ViewModels;

namespace Demo.Console
{
    public interface IApp
    {
        public Task Run();
    }
    public class App : IApp
    {
        private readonly ITelldusHttpClient _httpClient;

        public App(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Run()
        {
            UserRepository userRepository;
            StatusResponse status;
            DeviceRepository deviceRepository;
            string resourceParameter = null;

            // clients
            var clientRepository = new ClientRepository(_httpClient);
            var clients = await clientRepository.GetClientsAsync(resourceParameter);
            Print(clients);

            // sensors
            var sensorRepository = new SensorRepository(_httpClient);
            var sensors = await sensorRepository.GetSensorsAsync();
            Print(sensors);
            
            // events
            var eventRepository = new EventRepository(_httpClient);
            var events = await eventRepository.GetEventsAsync(resourceParameter);
            Print(events);
            
            // userhones":
            userRepository = new UserRepository(_httpClient);
            var phones = await userRepository.GetPhonesAsync();
            Print(phones);
            
            // userrofile":
            userRepository = new UserRepository(_httpClient);
            var profile = await userRepository.GetProfileAsync();
            Print(profile);
            
            // userushTest":
            userRepository = new UserRepository(_httpClient);
            resourceParameter = $"{phones.Phone.First().DeviceId},hej";
            var phoneId = resourceParameter.Split(",")[0];
            var message = resourceParameter.Split(",")[1];
            status = await userRepository.SendPushTest(phoneId, message);
            Print(status);
            
            // deviceon":
            deviceRepository = new DeviceRepository(_httpClient);
            status = await deviceRepository.TurnOnAsync(resourceParameter);
            Print(status);
            
            // deviceoff":
            deviceRepository = new DeviceRepository(_httpClient);
            status = await deviceRepository.TurnOffAsync(resourceParameter);
            Print(status);
        }

        private static void Print(object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            System.Console.WriteLine(json);
        }
    }
}
