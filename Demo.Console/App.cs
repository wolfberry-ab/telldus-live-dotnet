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
            //await CallClientRepository();
            //await CallDeviceRepository();
            //await CallEventRepository();
            await CallSensorRepository();
        }

        private async Task CallSensorRepository()
        {
            const string invalidSensorId = "invalidSensor";
            StatusResponse status;

            ISensorRepository sensorRepository = new SensorRepository(_httpClient);
            var sensors = await sensorRepository.GetSensorsAsync();
            Print(sensors, "GetSensors");

            var firstSensor = sensors.First();

            var sensorInfo = await sensorRepository.GetSensorInfoAsync(firstSensor.Id, true);
            Print(sensorInfo, "GetSensorInfo");

            try
            {
                const string name = "sensorY";
                status = await sensorRepository.SetNameAsync(invalidSensorId, name);
                Print(status, "SetName");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetName");
            }

            var sensorIdWithHistory = "1545389717";
            var history = await sensorRepository.GetHistoryAsync(sensorIdWithHistory, true, true, true);
            Print(history, "GetHistory");

            try
            {
                status = await sensorRepository.RemoveHistoryAsync(invalidSensorId);
                Print(status, "RemoveHistory");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveHistory");
            }

            try
            {
                const string timeUuid = "timeuiid";
                status = await sensorRepository.RemoveValueAsync(invalidSensorId, timeUuid);
                Print(status, "RemoveValue");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveValue");
            }

            try
            {
                status = await sensorRepository.ResetMaxMin(invalidSensorId, string.Empty);
                Print(status, "ResetMaxMin");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "ResetMaxMin");
            }

            try
            {
                status = await sensorRepository.SetKeepHistoryAsync(sensorIdWithHistory, true);
                Print(status, "SetKeepHistory");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetKeepHistory");
            }
        }

        private async Task CallEventRepository()
        {
            const string listOnly = null;
            IEventRepository eventRepository = new EventRepository(_httpClient);
            var events = await eventRepository.GetEventsAsync(listOnly);
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
                const string invalidClientId = "invalidClient";
                status = await deviceRepository.AddAsync(invalidClientId, "name", "transport", "zwave", "model", null);
            }
            catch (Exception e)
            {
                Print( e.ToString(), "AddDevice");
            }

            var deviceIdWithHistory = "4281891";
            var history = await deviceRepository.GetHistoryAsync(deviceIdWithHistory);
            Print(history, "GetHistory");

            var info = await deviceRepository.GetDeviceInfoAsync(onOffDeviceId);
            Print(info, "GetDeviceInfo");

            try
            {
                status = await deviceRepository.SetRgbAsync(deviceIdWithHistory, 100, 155, 240);
                Print(status, "SetRgb");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetRgb");
            }
            const string invalidDeviceId = "invalidDevice";

            // NOTE: The route might return success even if the deviceId is invalid
            try
            {
                const string model = "yyy";
                status = await deviceRepository.SetModelAsync(invalidDeviceId, model);
                Print(status, "SetModel");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetModel");
            }

            try
            {
                const bool ignore = false;
                status = await deviceRepository.IgnoreAsync(invalidDeviceId, ignore);
                Print(status, "SetIgnore");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetIgnore");
            }

            try
            {
                const string parameter = "test";
                const string value = "test";
                status = await deviceRepository.SetMetadataAsync(invalidDeviceId, parameter, value);
                Print(status, "SetMetadata");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetMetadata");
            }

            try
            {
                const string protocol = "zwave";
                status = await deviceRepository.SetProtocolAsync(invalidDeviceId, protocol);
                Print(status, "SetProtocol");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetProtocol");
            }

            try
            {
                const string mode = "heat";
                const string temperature = "20.0";
                const int changeMode = 1;
                status = await deviceRepository.SetThermostatAsync(invalidDeviceId, mode, temperature, null, changeMode);
                Print(status, "SetThermostat");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetThermostat");
            }
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

                const string clientId = "invalidClient";
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
