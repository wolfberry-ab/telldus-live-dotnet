using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Scheduler;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public class SchedulerRepository : BaseRepository, ISchedulerRepository
    {
        public SchedulerRepository(ITelldusHttpClient httpClient) : base(httpClient)
        {
            // Intentionally left blank
        }

        public async Task<JobResponse> GetJobAsync(
            string jobId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/scheduler/jobInfo");

            urlBuilder.AddQuery("id", jobId);

            var url = urlBuilder.Build();

            return await GetOrThrow<JobResponse>(url);
        }

        public async Task<JobsResponse> GetJobsAsync(string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/scheduler/jobList");

            var url = urlBuilder.Build();

            return await GetOrThrow<JobsResponse>(url);
        }

        public async Task<StatusResponse> RemoveJobAsync(
            string jobId,
            string format = ResponseFormat.JsonFormat)
        {
            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/scheduler/removeJob");

            urlBuilder.AddQuery("id", jobId);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }

        public async Task<StatusResponse> SetJobAsync(
            string jobId,
            string deviceId,
            string method,
            string methodValue,
            string type,
            int hour,
            int minute,
            int offset,
            int randomInterval,
            int retries,
            int retryInterval,
            int reps,
            bool active,
            string weekdays,
            string format = ResponseFormat.JsonFormat)
        {
            if (deviceId != null && jobId != null)
            {
                throw new ArgumentException("Only one of deviceId and jobId can be set");
            }

            var urlBuilder = new UrlBuilder($"{_httpClient.BaseUrl}/{format}/scheduler/setJob");

            urlBuilder.AddOptionalQuery("id", jobId);
            urlBuilder.AddOptionalQuery("deviceId", deviceId);
            urlBuilder.AddOptionalQuery("method", method);
            urlBuilder.AddOptionalQuery("methodValue", methodValue);
            urlBuilder.AddOptionalQuery("type", type);
            urlBuilder.AddQuery("hour", hour);
            urlBuilder.AddQuery("minute", minute);
            urlBuilder.AddQuery("offset", offset);
            urlBuilder.AddQuery("randomInterval", randomInterval);
            urlBuilder.AddQuery("retries", retries);
            urlBuilder.AddQuery("retryInterval", retryInterval);
            urlBuilder.AddQuery("active", active);
            urlBuilder.AddQuery("reps", reps);
            urlBuilder.AddQuery("active", active);
            urlBuilder.AddQuery("weekdays", weekdays);

            var url = urlBuilder.Build();

            return await GetOrThrow<StatusResponse>(url);
        }
    }
}
