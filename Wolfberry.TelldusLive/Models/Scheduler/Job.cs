namespace Wolfberry.TelldusLive.Models.Scheduler
{
    public class Job
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string Method { get; set; }
        public string MethodValue { get; set; }
        public int NextRunTime { get; set; }
        public string Type { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public int Offset { get; set; }
        public int RandomInterval { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
        public int Reps { get; set; }
        public int Active { get; set; }
        public string Weekdays { get; set; }
    }
}
