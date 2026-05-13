using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Event;
using Wolfberry.TelldusLive.Repositories;
using Wolfberry.TelldusLive.Utils;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class EventRepositoryTests
    {
        private const string MockedUrl = "https://mocked.url";

        private static ITelldusHttpClient CreateMockClient(string responseJson)
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(default).ReturnsForAnyArgs(responseJson);
            return client;
        }

        [Fact]
        public async Task RemoveActionAsync_ReturnsOk()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var mockedResponseJson = JsonUtil.Serialize(mockedResponse);
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl.Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(MockedUrl).ReturnsForAnyArgs(mockedResponseJson);
            var repository = new EventRepository(telldusClient);

            var status = await repository.RemoveActionAsync("123");

            Assert.Equal(mockedResponse.Status, status.Status);
        }

        [Fact]
        public async Task RemoveActionAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Action \"123\" not found!" };
            var mockedResponseJson = JsonUtil.Serialize(mockedResponse);
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl.Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(MockedUrl).ReturnsForAnyArgs(mockedResponseJson);
            var repository = new EventRepository(telldusClient);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.RemoveActionAsync("123"));
        }

        [Fact]
        public async Task RemoveActionAsync_EmptyResponse_ThrowsException()
        {
            var telldusClient = Substitute.For<ITelldusHttpClient>();
            telldusClient.BaseUrl.Returns(MockedUrl);
            telldusClient.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            var repository = new EventRepository(telldusClient);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.RemoveActionAsync("123"));
        }

        [Fact]
        public async Task GetEventsAsync_ReturnsEvents()
        {
            var mockedResponse = new EventsResponse
            {
                Event = new List<Event> { new Event { Id = "e1" } }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.GetEventsAsync(false);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEventsAsync_WithEventsOnly_ReturnsEvents()
        {
            var mockedResponse = new EventsResponse { Event = new List<Event>() };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.GetEventsAsync(true);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEventGroupListAsync_ReturnsGroups()
        {
            var mockedResponse = new EventGroupsResponse
            {
                EventGroup = new List<EventGroup> { new EventGroup { Id = "g1", Name = "Home" } }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.GetEventGroupListAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEventInfoAsync_ReturnsEventInfo()
        {
            var mockedResponse = new EventInfoResponse { Id = "e1", Description = "Test" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.GetEventInfoAsync("e1");

            Assert.NotNull(result);
            Assert.Equal("e1", result.Id);
        }

        [Fact]
        public async Task RemoveConditionAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.RemoveConditionAsync("cond1");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveEventAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.RemoveEventAsync("e1");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveGroupAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.RemoveGroupAsync("g1");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task RemoveTriggerAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.RemoveTriggerAsync("t1");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetBlockHeaterTriggerAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetBlockHeaterTriggerAsync("t1", "e1", "s1", 7, 30);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetDeviceActionAsync_InvalidRepeats_ThrowsArgumentException()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            await Assert.ThrowsAsync<ArgumentException>(
                () => repository.SetDeviceActionAsync(null, "e1", "d1", DeviceMethod.TurnOn, null, 0, null, DelayPolicy.Restart));
        }

        [Fact]
        public async Task SetDeviceActionAsync_ValidRepeats_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetDeviceActionAsync(null, "e1", "d1", DeviceMethod.TurnOn, null, 1, null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetDeviceConditionAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetDeviceConditionAsync(null, "e1", "group1", "d1", DeviceMethod.TurnOn);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetDeviceTriggerAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetDeviceTriggerAsync(null, "e1", "d1", DeviceMethod.TurnOn);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetEmailActionAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetEmailActionAsync(null, "e1", "user@example.com", "Alert!", null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetEventAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetEventAsync(null, "g1", "My event", true);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetGroupAsync_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetGroupAsync(null, "Home group");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeActionAsync_WithActionId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeActionAsync("a1", "e1", "obj1", "type1", "m1", true, 5, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeActionAsync_WithoutActionId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeActionAsync(null, "e1", "obj1", "type1", "m1", false, null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeConditionAsync_WithNullIds_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeConditionAsync(null, "e1", null, "obj1", "type1", "m1");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeConditionAsync_WithIds_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeConditionAsync("cond1", "e1", "group1", "obj1", "type1", "m1");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeTriggerAsync_WithNullId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeTriggerAsync(null, "e1", "type1", "obj1", "m1");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetModeTriggerAsync_WithId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetModeTriggerAsync("t1", "e1", "type1", "obj1", "m1");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetPushTriggerAsync_WithNullParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetPushTriggerAsync(null, "e1", "ph1", "Alert!", null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetPushTriggerAsync_WithParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetPushTriggerAsync("a1", "e1", "ph1", "Alert!", 5, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSmsActionAsync_WithNullParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSmsActionAsync(null, "e1", "46709000001", "Hi!", false, null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSmsActionAsync_WithParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSmsActionAsync("a1", "e1", "46709000001", "Hi!", true, 5, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSensorConditionAsync_WithAllNulls_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSensorConditionAsync(null, "e1", null, "s1", true, Edge.Rising, "temp", null);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSensorConditionAsync_WithAllParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSensorConditionAsync("cond1", "e1", "group1", "s1", false, Edge.Falling, "temp", "0");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSensorTriggerAsync_WithNullParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSensorTriggerAsync(null, "e1", "s1", "20", Edge.Rising, "temp", null);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSensorTriggerAsync_WithAllParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSensorTriggerAsync("t1", "e1", "s1", "20", Edge.Rising, "temp", "0");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSuntimeConditionAsync_WithNullId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSuntimeConditionAsync(null, "e1", "group1", SunStatus.Up, 0, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSuntimeConditionAsync_WithId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSuntimeConditionAsync("cond1", "e1", "group1", SunStatus.Down, 10, -10);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSuntimeTriggerAsync_WithNullId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSuntimeTriggerAsync(null, "e1", "c1", SunStatus.Up, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetSuntimeTriggerAsync_WithId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetSuntimeTriggerAsync("t1", "e1", "c1", SunStatus.Up, 15);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetTimeConditionAsync_WithNullId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetTimeConditionAsync(null, "e1", "group1", 8, 0, 10, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetTimeConditionAsync_WithId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetTimeConditionAsync("cond1", "e1", "group1", 8, 0, 22, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetUrlActionAsync_WithNullParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetUrlActionAsync(null, "e1", "https://hook.example.com", null, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetUrlActionAsync_WithParams_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetUrlActionAsync("a1", "e1", "https://hook.example.com", 5, DelayPolicy.Restart);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetWeekdayConditionAsync_WithNullId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetWeekdayConditionAsync(null, "e1", "group1", "1,2,3,4,5");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetWeekdayConditionAsync_WithId_ReturnsCreatedResponse()
        {
            var mockedResponse = new CreatedResponse { Id = "cr1" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            IEventRepository repository = new EventRepository(client);

            var result = await repository.SetWeekdayConditionAsync("cond1", "e1", "group1", "1,2,3,4,5");

            Assert.NotNull(result);
        }
    }
}
