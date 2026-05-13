// Example: Read sensor values and history
//
// Install: dotnet add package Wolfberry.TelldusLive

using Wolfberry.TelldusLive;

var client = new TelldusLiveClient(
    consumerKey:       Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY")!,
    consumerKeySecret: Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY_SECRET")!,
    accessToken:       Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN")!,
    accessTokenSecret: Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN_SECRET")!);

// List all sensors with current readings
var sensors = await client.Sensors.GetSensorsAsync(
    includeIgnored: false,
    includeValues: true,
    includeScale: true,
    includeUnit: true);

foreach (var sensor in sensors.Sensor)
{
    Console.WriteLine($"{sensor.Name}:");
    foreach (var data in sensor.Data)
    {
        Console.WriteLine($"  {data.Name}: {data.Value} {data.Unit}");
    }
}

// Get history for a specific sensor (API rate limit: once per 10 minutes)
var sensorId = "12345"; // replace with your sensor ID
var history = await client.Sensors.GetHistoryAsync(
    sensorId,
    includeKey: true,
    includeUnit: true,
    includeHumanReadableDate: true);

foreach (var entry in history.History)
{
    Console.WriteLine($"{entry.LocalDate}:");
    foreach (var data in entry.Data)
    {
        Console.WriteLine($"  {data.Name}: {data.Value} {data.Unit}");
    }
}
