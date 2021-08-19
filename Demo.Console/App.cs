using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Repositories;

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
            string resourceParameter = null;
            // TODO: Remove resourceParameter from repository API:s

            await CallClientRepository();
            await CallDeviceRepository();
            await CallEventRepository();

            return; 

            // sensors
            var sensorRepository = new SensorRepository(_httpClient);
            var sensors = await sensorRepository.GetSensorsAsync();
            Print(sensors);

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
            status = await userRepository.SendPushTestAsync(phoneId, message);
            Print(status);

        }

        private async Task CallEventRepository()
        {
            const string resourceParameter = null;
            IEventRepository eventRepository = new EventRepository(_httpClient);
            var events = await eventRepository.GetEventsAsync(resourceParameter);
            Print(events);

            var eventGroups = await eventRepository.GetEventGroupListAsync();
            Print(eventGroups, "GetEventGroupLists");

            const string eventId = "785305";
            var eventInfo = await eventRepository.GetEventInfoAsync(eventId);
            Print(eventInfo, "GetEventInfo");

            try
            {
                const string actionId = "dummy";
                var status = await eventRepository.RemoveActionAsync(actionId);
                Print(status, "RemoveAction");
            }
            catch (Exception)
            {
                Print("{}", "RemoveAction");
            }
        }

        private async Task CallDeviceRepository()
        {
            IDeviceRepository deviceRepository = new DeviceRepository(_httpClient);
            StatusResponse status = null;

            const bool includeIgnored = true;
            const string supportedMethods = null;
            const string extras = null;
            var devices = await deviceRepository.GetDevicesAsync(includeIgnored, supportedMethods, extras);
            Print(devices, "GetDevices");
            var onOffDeviceId = devices.Device
                .Single(x => "N1 ELLEN".Equals(x.Name))
                .Id;

            status = await deviceRepository.TurnOnAsync(onOffDeviceId);
            Print(status, "TurnOnAsync");

            deviceRepository = new DeviceRepository(_httpClient);
            status = await deviceRepository.TurnOffAsync(onOffDeviceId);
            Print(status, "TurnOffAsync");

            try
            {
                const string invalidClientId = "xxx";
                status = await deviceRepository.AddAsync(invalidClientId, "name", "transport", "zwave", "model", null);
            }
            catch (Exception e)
            {
                Print( e.ToString(), "AddDevice");
            }

            var deviceIdWithHistory = "4281891";
            var history = await deviceRepository.GetHistoryAsync(deviceIdWithHistory);
            Print(history, "GetHistory");

            var info = await deviceRepository.GetDeviceInfo(onOffDeviceId);
            Print(info, "GetDeviceInfo");
        }

        private async Task CallClientRepository()
        {
            StatusResponse status;
            const string extras = "timezone";

            // clients
            IClientRepository clientRepository = new ClientRepository(_httpClient);
            var clients = await clientRepository.GetClientsAsync(extras);
            Print(clients, "Clients");

            var firstClientId = clients.Client.First().Id;

            var clientInfo = await clientRepository.GetClientInfoAsync(firstClientId, extras: extras);
            Print(clientInfo, "Client Info");
            try
            {

                const string clientId = "xxx";
                const string uuid = "yyy";
                var registration = await clientRepository.RegisterAsync(clientId, uuid);
                Print(registration, "Register");

                var removal = await clientRepository.RemoveAsync(clientId);
                Print(removal, "Remove");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            const double longitude = 13.19;
            const double latitude = 55.70;
            status = await clientRepository.SetCoordinatesAsync(firstClientId,
                                                                longitude,
                                                                latitude);
            Print(status, "SetCoordinates");

            const string name = "Stickan V1";
            status = await clientRepository.SetNameAsync(firstClientId, name);
            Print(status, "SetName");

            try
            {
                const bool enablePush = true;
                status = await clientRepository.EnablePushAsync(firstClientId, enablePush);
                Print(status, "EnablePush");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "EnablePush");
            }

            const string timezone = "Europe/Stockholm";
            status = await clientRepository.SetTimezoneAsync(firstClientId, timezone);
            Print(status, "SetTimezone");

            try
            {
                const string email = "info@wolfberry.se";
                const string invalidClientId = "asdf";
                status = await clientRepository.TransferAsync(invalidClientId, email);
                Print(status, "Transfer");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "Transfer");
            }
        }

        private static void Print(object data, string description = "")
        {
            System.Console.BackgroundColor = ConsoleColor.Green;
            System.Console.ForegroundColor = ConsoleColor.Black;
            System.Console.Write(description);
            System.Console.ResetColor();
            System.Console.WriteLine();

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            System.Console.WriteLine(json);
        }
    }
}
