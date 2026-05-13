using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Scheduler;
using Wolfberry.TelldusLive.Repositories;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Repositories
{
    public class SchedulerRepositoryTests
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
        public async Task GetJobsAsync_ReturnsJobs()
        {
            var mockedResponse = new JobsResponse
            {
                Job = new List<Job>
                {
                    new Job { Id = "10", DeviceId = "100" }
                }
            };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            var result = await repository.GetJobsAsync();

            Assert.NotNull(result);
            Assert.Equal("10", result.Job.First().Id);
        }

        [Fact]
        public async Task GetJobAsync_ReturnsJob()
        {
            var mockedResponse = new JobResponse { Id = "10", DeviceId = "100" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            var result = await repository.GetJobAsync("10");

            Assert.Equal("10", result.Id);
            Assert.Equal("100", result.DeviceId);
        }

        [Fact]
        public async Task RemoveJobAsync_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            var result = await repository.RemoveJobAsync("10");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetJobAsync_BothIdsProvided_ThrowsArgumentException()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            await Assert.ThrowsAsync<ArgumentException>(
                () => repository.SetJobAsync("10", "100", "1", null, "time",
                    8, 0, 0, 0, 3, 5, 1, true, "1,2,3,4,5"));
        }

        [Fact]
        public async Task GetJobsAsync_ErrorResponse_ThrowsException()
        {
            var mockedResponse = new ErrorResponse { Error = "Access denied" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetJobsAsync());
        }

        [Fact]
        public async Task GetJobAsync_NullResponse_ThrowsException()
        {
            var client = Substitute.For<ITelldusHttpClient>();
            client.BaseUrl.Returns(MockedUrl);
            client.GetAsJsonAsync(Arg.Any<string>()).ReturnsNull();
            ISchedulerRepository repository = new SchedulerRepository(client);

            await Assert.ThrowsAsync<RepositoryException>(
                () => repository.GetJobAsync("10"));
        }

        [Fact]
        public async Task SetJobAsync_OnlyDeviceId_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            var result = await repository.SetJobAsync(null, "100", "1", null, "time",
                8, 0, 0, 0, 3, 5, 1, true, "1,2,3,4,5");

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public async Task SetJobAsync_OnlyJobId_ReturnsSuccess()
        {
            var mockedResponse = new StatusResponse { Status = "success" };
            var client = CreateMockClient(JsonConvert.SerializeObject(mockedResponse));
            ISchedulerRepository repository = new SchedulerRepository(client);

            var result = await repository.SetJobAsync("10", null, "1", null, "time",
                8, 0, 0, 0, 3, 5, 1, true, "1,2,3,4,5");

            Assert.Equal("success", result.Status);
        }
    }
}
