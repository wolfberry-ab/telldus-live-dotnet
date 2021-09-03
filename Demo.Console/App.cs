using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Event;
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
            await CallEventRepository();
            //await CallSensorRepository();
            //await CallUserRepository();
            //await CallSchedulerRepository();
            //await CallGroupRepository();
        }

        private async Task CallGroupRepository()
        {
            StatusResponse status;
            IGroupRepository groupRepository = new GroupRepository(_httpClient);

            const string invalidClientId = "invalidClientId";

            try
            {
                const string name = "groupName";
                const string devices = "1,2,3,4";
                status = await groupRepository.AddGroupAsync(invalidClientId, name, devices);
                Print(status, "AddGroup");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "AddGroup");
            }

            try
            {
                const string invalidGroupId = "invalidGroupId";
                status = await groupRepository.RemoveGroupAsync(invalidGroupId);
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveGroup");
            }
        }

        private async Task CallSchedulerRepository()
        {
            ISchedulerRepository schedulerRepository = new SchedulerRepository(_httpClient);
            StatusResponse status;

            var jobs = await schedulerRepository.GetJobsAsync();
            Print(jobs, "ListJobs");

            var firstJob = jobs.Job.First();
            var jobInfo = await schedulerRepository.GetJobAsync(firstJob.Id);
            Print(jobInfo, "JobInfo");

            const string invalidJobId = "invalidJobId";
            try
            {
                status = await schedulerRepository.RemoveJobAsync(invalidJobId);
                Print(status, "RemoveJob");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveJob");
            }

            try
            {
                const string invalidDeviceId = "invalidDeviceId";
                status = await schedulerRepository.SetJobAsync(
                    null,
                    invalidDeviceId,
                    null, 
                    null,
                    null,
                    1,
                    2,
                    3, 
                    4,
                    5,
                    6,
                    7,
                    true,
                    "1");
                Print(status, "SetJobAsync");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetJobAsync");
            }
        }

        private async Task CallUserRepository()
        {
            IUserRepository userRepository = new UserRepository(_httpClient);
            StatusResponse status;

            var history = await userRepository.GetSmsHistoryAsync();
            Print(history, "GetSmsHistory");

            var eula = await userRepository.GetEulaAsync();
            Print(eula, "GetEula");

            status = await userRepository.AcceptEulaAsync(2);
            Print(status, "AcceptEula");

            try
            {
                const string invalidCode = "invalidCode";
                status = await userRepository.ActivateCouponAsync(invalidCode);
            }
            catch (Exception e)
            {
                Print(e.ToString(), "ActivateCoupon");
            }
            // Intentionally inactivated
            //status = await userRepository.ChangeEmailAsync("telldus@wolfberry.se");
            //Print(status, "ChangeEmail");

            //const string locale = "auto";
            //status = await userRepository.ChangeLocaleAsync(locale);
            //Print(status, "ChangeLocale");

            //const string currentPassword = "invalidPassword";
            //const string newPassword = "newPassword";
            //status = await userRepository.ChangePasswordAsync(currentPassword, newPassword);

            try
            {
                const string invalidPushToken = "invalidPushToken";
                status = await userRepository.DeletePushTokenAsync(invalidPushToken);
                Print(status, "DeletePushToken");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "DeletePushToken");
            }

            try
            {
                status = await userRepository.RegisterPushTokenAsync(null, null, null, null, null, null, null);
                Print(status, "RegisterPushToken");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RegisterPushToken");
            }

            try
            {
                status = await userRepository.SendPushTestAsync(null, null);
                Print(status, "SendPushTest");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SendPushTest");
            }

            const string firstName = "Mikael";
            const string lastName = "Johansson";
            status = await userRepository.SetNameAsync(firstName, lastName);
            Print(status, "SetName");

            try
            {
                status = await userRepository.UnregisterPushToken(null);
                Print(status, "UnregisterPushToken");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "UnregisterPushToken");
            }
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
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveAction");
            }

            try
            {
                const string invalidConditionId = "invalid";
                var status = await eventRepository.RemoveConditionAsync(invalidConditionId);
                Print(status, "RemoveCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveCondition");
            }

            try
            {
                const string invalidEventId = "invalid";
                var status = await eventRepository.RemoveEventAsync(invalidEventId);
                Print(status, "RemoveEvent");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveEvent");
            }

            try
            {
                const string invalidGroupId = "invalid";
                var status = await eventRepository.RemoveGroupAsync(invalidGroupId);
                Print(status, "RemoveGroup");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveGroup");
            }

            try
            {
                const string invalidTriggerId = "invalid";
                var status = await eventRepository.RemoveTriggerAsync(invalidTriggerId);
                Print(status, "RemoveTrigger");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "RemoveTrigger");
            }

            const string invalidId = "invalid";

            try
            {
                var status = await eventRepository.SetBlockHeaterTriggerAsync(
                    invalidId, invalidId, invalidId, 1, 1);
                Print(status, "SetBlockHeaterTrigger");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetBlockHeaterTrigger");
            }

            try
            {
                var status = await eventRepository.SetDeviceActionAsync(
                    invalidId, invalidId, invalidId, DeviceMethod.Thermostat, null,
                    1, 10, "repeat");
                Print(status, "SetDeviceAction");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetDeviceAction");
            }

            try
            {
                const string invalidGroup = "invalidGroup";
                var status = await eventRepository.SetDeviceConditionAsync(
                    invalidId, invalidId, invalidGroup, invalidId, DeviceMethod.Bell);
                Print(status, "SetDeviceCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetDeviceCondition");
            }

            try
            {
                const string emailAddress = "mail@mail.com";
                const string message = "Hi";
                var status = await eventRepository.SetEmailActionAsync(
                        invalidId, invalidId, emailAddress, message, 10, "repeat");
                Print(status, "SetEmailAction");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetEmailAction");
            }

            try
            {
                const string removeGroupId = "00000000-0000-0000-0000-000000000000";
                const string description = "A test event";
                var status = await eventRepository.SetEventAsync(invalidId, removeGroupId, description, true);
                Print(status, "SetEvent");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetEvent");
            }

            try
            {
                var status = await eventRepository.SetGroupAsync(invalidId, "Fail to update");
                Print(status, "SetGroup");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetGroup");
            }

            try
            {
                const string objectType = "room";
                var status = await eventRepository.SetModeActionAsync(
                    invalidId, invalidId, invalidId, objectType, invalidId, true, 10,
                    "repeat");
                Print(status, "SetModeAction");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetModeAction");
            }

            try
            {
                const string group = "group";
                const string objectType = "room";
                var status = await eventRepository.SetModeConditionAsync(
                    invalidId, invalidId, group, invalidId, objectType, invalidId, true);
                Print(status, "SetModeCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetModeCondition");
            }

            try
            {
                const string objectType = "room";
                var status = await eventRepository.SetModeTriggerAsync(
                    invalidId, invalidId, objectType, invalidId, invalidId);
                Print(status, "SetModeTrigger");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetModeTrigger");
            }

            try
            {
                const string message = "Hi there";
                var status = await eventRepository.SetPushTriggerAsync(
                    invalidId, invalidId, invalidId, message, 10, "restart");
                Print(status, "SetPushTrigger");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetPushTrigger");
            }

            try
            {
                const string message = "Hi there";
                var status = await eventRepository.SetSmsActionAsync(
                    invalidId, invalidId, "46708445588", message, true, 10, "restart");
                Print(status, "SetSmsAction");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetSmsAction");
            }

            try
            {
                var status = await eventRepository.SetSensorConditionAsync(
                    invalidId, invalidId,"group", invalidId, true, 
                    Edge.Equal, "temp");
                Print(status, "SetSensorCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetSensorCondition");
            }

            try
            {
                var status = await eventRepository.SetSensorTriggerAsync(
                    invalidId, invalidId, invalidId, "10", Edge.Equal, "temp");
                Print(status, "SetSensorTrigger");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetSensorTrigger");
            }

            try
            {
                var status = await eventRepository.SetTimeConditionAsync(
                    invalidId, invalidId, "group", 1, 59, 2,59);
                Print(status, "SetTimeCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetTimeCondition");
            }

            try
            {
                var status = await eventRepository.SetUrlActionAsync(
                    invalidId, invalidId, "http://localhost", 10, "restart");
                Print(status, "SetUrlAction");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetUrlAction");
            }

            try
            {
                var status = await eventRepository.SetWeekdayConditionAsync(
                    invalidId, invalidId, "group", "2,3,4");
                Print(status, "SetWeekdayCondition");
            }
            catch (Exception e)
            {
                Print(e.ToString(), "SetWeekdayCondition");
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
