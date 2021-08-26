using System;
using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Device;
using Wolfberry.TelldusLive.Models.Scheduler;

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
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/scheduler/jobInfo?id={jobId}";

            return await GetOrThrow<JobResponse>(requestUri);
        }

        public async Task<JobsResponse> GetJobsAsync(string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/scheduler/jobList";

            return await GetOrThrow<JobsResponse>(requestUri);
        }

        public async Task<StatusResponse> RemoveJobAsync(
            string jobId,
            string format = Constraints.JsonFormat)
        {
            var requestUri = $"{_httpClient.BaseUrl}/{format}/scheduler/removeJob?id={jobId}";

            return await GetOrThrow<StatusResponse>(requestUri);
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
            string format = Constraints.JsonFormat)
        {
            if (deviceId != null && jobId != null)
            {
                throw new ArgumentException("Only one of deviceId and jobId can be set");
            }

            var requestUri = $"{_httpClient.BaseUrl}/{format}/scheduler/setJob";

            requestUri += $"?offset={offset}";

            if (jobId != null)
            {
                requestUri += $"&id={jobId}";
            }

            if (deviceId != null)
            {
                requestUri += $"&deviceId={deviceId}";
            }

            if (method != null)
            {
                requestUri += $"&method={method}";
            }

            if (methodValue != null)
            {
                requestUri += $"&methodValue={methodValue}";
            }

            if (type != null)
            {
                requestUri += $"&type={type}";
            }

            requestUri += $"&hour={hour}&minute={minute}&offset={offset}";
            requestUri += $"&randomInterval={randomInterval}";
            requestUri += $"&retries={retries}&retryInterval={retryInterval}";
            requestUri += $"&reps={reps}";

            var activeNumber = active ? 1 : 0;
            requestUri += $"&active={activeNumber}";
            requestUri += $"&weekdays={weekdays}";

            return await GetOrThrow<StatusResponse>(requestUri);
        }
    }
}
