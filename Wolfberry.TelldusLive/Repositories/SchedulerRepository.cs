namespace Wolfberry.TelldusLive.Repositories
{
    public class SchedulerRepository : ISchedulerRepository
    {
        private readonly ITelldusHttpClient _httpClient;

        public SchedulerRepository(ITelldusHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // scheduler/jobList
    }
}
