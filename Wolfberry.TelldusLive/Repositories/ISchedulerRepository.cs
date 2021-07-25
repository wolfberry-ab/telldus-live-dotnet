using System.Threading.Tasks;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Models.Scheduler;

namespace Wolfberry.TelldusLive.Repositories
{
    public interface ISchedulerRepository
    {
        /// <summary>
        /// Get specified job
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<JobResponse> GetJobAsync(
            string jobId, 
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Get all jobs
        /// </summary>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<JobsResponse> GetJobsAsync(string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Remove specified job
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> RemoveJobAsync(
            string jobId,
            string format = ResponseFormat.JsonFormat);

        /// <summary>
        /// Create or update specified job. NOTE: Not tested
        /// </summary>
        /// <param name="jobId">Job ID or 0 to create a new job</param>
        /// <param name="deviceId">Must and can only be set if creating a new job</param>
        /// <param name="method">What to do when the job runs</param>
        /// <param name="methodValue">See DeviceMethod class</param>
        /// <param name="type">"time", "sunrise" or "sunset"</param>
        /// <param name="hour">0-23</param>
        /// <param name="minute">0-59</param>
        /// <param name="offset">Value between -1439-1439. Only used when type "sunrise" or "sunset" is set.</param>
        /// <param name="randomInterval"></param>
        /// <param name="retries"></param>
        /// <param name="retryInterval"></param>
        /// <param name="reps"></param>
        /// <param name="active">true if active, false if paused</param>
        /// <param name="weekdays">Comma separated list of weekdays. 1 is monday. Example: 2,3,4</param>
        /// <param name="format">json (default) or xml</param>
        /// <returns></returns>
        Task<StatusResponse> SetJobAsync(
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
            string format = ResponseFormat.JsonFormat);
    }
}