using System.Collections.Generic;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Client;
using Wolfberry.TelldusLive.Models.Device;
using Wolfberry.TelldusLive.Models.Event;
using Wolfberry.TelldusLive.Models.Scheduler;
using Wolfberry.TelldusLive.Models.Sensor;
using Wolfberry.TelldusLive.Models.User;
using Xunit;
using ClientModel = Wolfberry.TelldusLive.Models.Client.Client;
using DeviceHistoryEntry = Wolfberry.TelldusLive.Models.Device.HistoryEntry;
using SensorHistoryEntry = Wolfberry.TelldusLive.Models.Sensor.HistoryEntry;

namespace Wolfberry.TelldusLive.Tests.Models
{
    public class ModelTests
    {
        // ---- Top-level models ----

        [Fact]
        public void StatusResponse_CanInstantiate()
        {
            var model = new StatusResponse { Status = "success" };
            Assert.Equal("success", model.Status);
        }

        [Fact]
        public void ErrorResponse_CanInstantiate()
        {
            var model = new ErrorResponse { Error = "not found" };
            Assert.Equal("not found", model.Error);
        }

        [Fact]
        public void Action_CanInstantiate()
        {
            var model = new Action { Id = "1", Type = "push", Delay = "10", DelayPolicy = "restart" };
            Assert.Equal("1", model.Id);
            Assert.Equal("10", model.Delay);
            Assert.Equal("restart", model.DelayPolicy);
        }

        [Fact]
        public void Condition_CanInstantiate()
        {
            var model = new Condition { Id = "2", Type = "device", DeviceId = "d1", Method = "1", Group = "g1" };
            Assert.Equal("2", model.Id);
            Assert.Equal("d1", model.DeviceId);
            Assert.Equal("1", model.Method);
            Assert.Equal("g1", model.Group);
        }

        [Fact]
        public void DeviceTrigger_CanInstantiate()
        {
            var model = new DeviceTrigger { DeviceId = "10", Method = "1" };
            Assert.Equal("10", model.DeviceId);
        }

        [Fact]
        public void PushAction_CanInstantiate()
        {
            var model = new PushAction { PhoneId = "p1", Message = "hello" };
            Assert.Equal("p1", model.PhoneId);
        }

        [Fact]
        public void SensorTrigger_CanInstantiate()
        {
            var model = new SensorTrigger { SensorId = "s1", Value = "20", Edge = "1", ValueType = "temp", ReloadValue = "1", Scale = "0" };
            Assert.Equal("s1", model.SensorId);
            Assert.Equal("1", model.Edge);
            Assert.Equal("temp", model.ValueType);
            Assert.Equal("1", model.ReloadValue);
            Assert.Equal("0", model.Scale);
        }

        [Fact]
        public void Trigger_CanInstantiate()
        {
            var model = new Trigger { Id = "t1", Type = "sensor", ClientId = "c1" };
            Assert.Equal("t1", model.Id);
            Assert.Equal("c1", model.ClientId);
        }

        // ---- Client models ----

        [Fact]
        public void ClientModel_CanInstantiate()
        {
            var model = new ClientModel { Id = "c1", Name = "TellStick" };
            Assert.Equal("c1", model.Id);
        }

        [Fact]
        public void ClientInfoResponse_CanInstantiate()
        {
            var model = new ClientInfoResponse { Id = "c2", Name = "Hub" };
            Assert.Equal("c2", model.Id);
        }

        [Fact]
        public void ClientsResponse_CanInstantiate()
        {
            var model = new ClientsResponse { Client = new List<ClientModel>() };
            Assert.NotNull(model.Client);
        }

        // ---- Device models ----

        [Fact]
        public void Device_CanInstantiate()
        {
            var model = new Device { Id = "d1", Name = "Lamp" };
            Assert.Equal("d1", model.Id);
        }

        [Fact]
        public void DevicesResponse_CanInstantiate()
        {
            var model = new DevicesResponse { Device = new List<Device>() };
            Assert.NotNull(model.Device);
        }

        [Fact]
        public void DeviceResponse_CanInstantiate()
        {
            var model = new DeviceResponse { Id = "d2", Name = "Switch" };
            Assert.Equal("d2", model.Id);
        }

        [Fact]
        public void DeviceHistoryEntry_CanInstantiate()
        {
            var model = new DeviceHistoryEntry { Ts = 1000, State = 1 };
            Assert.Equal(1000, model.Ts);
        }

        [Fact]
        public void DeviceHistoryResponse_CanInstantiate()
        {
            var model = new HistoryResponse { History = new List<DeviceHistoryEntry>() };
            Assert.NotNull(model.History);
        }

        [Fact]
        public void Parameter_CanInstantiate()
        {
            var model = new Parameter { Name = "param", Value = "val" };
            Assert.Equal("param", model.Name);
        }

        [Fact]
        public void StateValue_CanInstantiate()
        {
            var model = new StateValue { State = "2", Value = "" };
            Assert.Equal("2", model.State);
        }

        // ---- Event models ----

        [Fact]
        public void CreatedResponse_CanInstantiate()
        {
            var model = new CreatedResponse { Id = "abc-123" };
            Assert.Equal("abc-123", model.Id);
        }

        [Fact]
        public void DelayPolicy_CanAccess()
        {
            Assert.Equal("restart", DelayPolicy.Restart);
            Assert.Equal("continue", DelayPolicy.Continue);
        }

        [Fact]
        public void EventModel_CanInstantiate()
        {
            var model = new Event { Id = "e1", Description = "Alarm" };
            Assert.Equal("e1", model.Id);
        }

        [Fact]
        public void EventGroup_CanInstantiate()
        {
            var model = new EventGroup { Id = "g1", Name = "Home" };
            Assert.Equal("g1", model.Id);
        }

        [Fact]
        public void EventGroupsResponse_CanInstantiate()
        {
            var model = new EventGroupsResponse { EventGroup = new List<EventGroup>() };
            Assert.NotNull(model.EventGroup);
        }

        [Fact]
        public void EventInfoResponse_CanInstantiate()
        {
            var model = new EventInfoResponse { Id = "ei1", Description = "Info" };
            Assert.Equal("ei1", model.Id);
        }

        [Fact]
        public void EventsResponse_CanInstantiate()
        {
            var model = new EventsResponse { Event = new List<Event>() };
            Assert.NotNull(model.Event);
        }

        [Fact]
        public void SetGroupResponse_CanInstantiate()
        {
            var model = new SetGroupResponse { Id = "sg1" };
            Assert.Equal("sg1", model.Id);
        }

        // ---- Scheduler models ----

        [Fact]
        public void Job_CanInstantiate()
        {
            var model = new Job { Id = "10", DeviceId = "100" };
            Assert.Equal("10", model.Id);
        }

        [Fact]
        public void JobResponse_CanInstantiate()
        {
            var model = new JobResponse { Id = "11", DeviceId = "101" };
            Assert.Equal("11", model.Id);
        }

        [Fact]
        public void JobsResponse_CanInstantiate()
        {
            var model = new JobsResponse { Job = new List<Job>() };
            Assert.NotNull(model.Job);
        }

        // ---- Sensor models ----

        [Fact]
        public void HistoryData_CanInstantiate()
        {
            var model = new HistoryData { Name = "temp", Value = "20.5", Scale = "0", Unit = "°C" };
            Assert.Equal("temp", model.Name);
            Assert.Equal("0", model.Scale);
            Assert.Equal("°C", model.Unit);
        }

        [Fact]
        public void SensorHistoryEntry_CanInstantiate()
        {
            var model = new SensorHistoryEntry { Ts = 2000, Uuid = "uuid-1" };
            Assert.Equal(2000, model.Ts);
        }

        [Fact]
        public void Sensor_CanInstantiate()
        {
            var model = new Sensor { Id = "s1", Name = "Temperature" };
            Assert.Equal("s1", model.Id);
        }

        [Fact]
        public void SensorData_CanInstantiate()
        {
            var model = new SensorData
            {
                Name = "humidity", Value = "55", Scale = "0",
                LastUpdated = 1000, Max = "80", MaxTime = "ts1",
                Min = "40", MinTime = "ts2", Unit = "%"
            };
            Assert.Equal("humidity", model.Name);
            Assert.Equal("0", model.Scale);
            Assert.Equal(1000, model.LastUpdated);
            Assert.Equal("80", model.Max);
            Assert.Equal("ts1", model.MaxTime);
            Assert.Equal("40", model.Min);
            Assert.Equal("ts2", model.MinTime);
            Assert.Equal("%", model.Unit);
        }

        [Fact]
        public void SensorHistoryResponse_CanInstantiate()
        {
            var model = new SensorHistoryResponse { History = new List<SensorHistoryEntry>() };
            Assert.NotNull(model.History);
        }

        [Fact]
        public void SensorsResponse_CanInstantiateWithList()
        {
            var model = new SensorsResponse(new List<Sensor>());
            Assert.NotNull(model.Sensors);
        }

        [Fact]
        public void TelldusSensorsResponse_CanInstantiate()
        {
            var model = new TelldusSensorsResponse { Sensor = new List<Sensor>() };
            Assert.NotNull(model.Sensor);
        }

        [Fact]
        public void SensorResponse_CanInstantiate()
        {
            var model = new SensorResponse { Id = "sr1", Name = "Outdoor" };
            Assert.Equal("sr1", model.Id);
        }

        // ---- User models ----

        [Fact]
        public void EulaResponse_CanInstantiate()
        {
            var model = new EulaResponse { Version = 3, Text = "Terms" };
            Assert.Equal(3, model.Version);
        }

        [Fact]
        public void Phone_CanInstantiate()
        {
            var model = new Phone { Id = "ph1", Name = "iPhone" };
            Assert.Equal("ph1", model.Id);
        }

        [Fact]
        public void PhonesResponse_CanInstantiate()
        {
            var model = new PhonesResponse { Phone = new List<Phone>() };
            Assert.NotNull(model.Phone);
        }

        [Fact]
        public void ProfileResponse_CanInstantiate()
        {
            var model = new ProfileResponse { Firstname = "John", Lastname = "Doe" };
            Assert.Equal("John", model.Firstname);
        }

        [Fact]
        public void SmsHistoryEntry_CanInstantiate()
        {
            var model = new SmsHistoryEntry { Id = "sms1", To = "46709123456" };
            Assert.Equal("sms1", model.Id);
        }

        [Fact]
        public void SmsHistoryEntryResponse_CanInstantiate()
        {
            var model = new SmsHistoryEntryResponse { History = new List<SmsHistoryEntry>() };
            Assert.NotNull(model.History);
        }

        [Fact]
        public void Uela_CanInstantiate()
        {
            var model = new Uela { Version = 2, Text = "Agreement" };
            Assert.Equal(2, model.Version);
        }

        // ---- Authentication models ----

        [Fact]
        public void TelldusOAuth1Configuration_CanInstantiate()
        {
            var config = new TelldusOAuth1Configuration
            {
                ConsumerKey = "key",
                ConsumerKeySecret = "secret",
                AccessToken = "token",
                AccessTokenSecret = "tokensecret",
                AccessTokenUrl = "https://example.com/access",
                AuthorizeTokenUrl = "https://example.com/auth",
                RequestTokenUrl = "https://example.com/request"
            };

            Assert.Equal("key", config.ConsumerKey);
            Assert.Equal("secret", config.ConsumerKeySecret);
        }
    }
}
