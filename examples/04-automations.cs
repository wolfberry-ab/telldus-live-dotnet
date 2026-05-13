// Example: Inspect automations — events and scheduled jobs
//
// Install: dotnet add package Wolfberry.TelldusLive

using Wolfberry.TelldusLive;

var client = new TelldusLiveClient(
    consumerKey:       Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY")!,
    consumerKeySecret: Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY_SECRET")!,
    accessToken:       Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN")!,
    accessTokenSecret: Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN_SECRET")!);

// List all automation events
var events = await client.Events.GetEventsAsync(eventsOnly: false);

Console.WriteLine("=== Events ===");
foreach (var evt in events.Event)
{
    Console.WriteLine($"{evt.Id,-10} {evt.Description} (active={evt.Active})");
}

// Get full detail on one event (triggers, conditions, actions)
if (events.Event.Count > 0)
{
    var detail = await client.Events.GetEventInfoAsync(events.Event[0].Id);
    Console.WriteLine($"\nTriggers: {detail.Trigger?.Count ?? 0}");
    Console.WriteLine($"Conditions: {detail.Condition?.Count ?? 0}");
    Console.WriteLine($"Actions: {detail.Action?.Count ?? 0}");
}

// List scheduled jobs
Console.WriteLine("\n=== Scheduled Jobs ===");
var jobs = await client.Scheduler.GetJobsAsync();
foreach (var job in jobs.Job)
{
    Console.WriteLine($"{job.Id,-10} device={job.DeviceId} {job.Hour:D2}:{job.Minute:D2} active={job.Active}");
}
