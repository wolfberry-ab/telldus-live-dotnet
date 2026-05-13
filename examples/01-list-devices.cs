// Example: List all devices and their current state
//
// Run: dotnet script or paste into Program.cs of a console app
// Install: dotnet add package Wolfberry.TelldusLive

using Wolfberry.TelldusLive;

var client = new TelldusLiveClient(
    consumerKey:       Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY")!,
    consumerKeySecret: Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY_SECRET")!,
    accessToken:       Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN")!,
    accessTokenSecret: Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN_SECRET")!);

var response = await client.Devices.GetDevicesAsync(includeIgnored: false);

foreach (var device in response.Device)
{
    Console.WriteLine($"{device.Id,-10} {device.Name,-30} state={device.State}");
}
