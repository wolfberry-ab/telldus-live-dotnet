// Example: Control devices — on/off, dim, RGB, blinds
//
// Install: dotnet add package Wolfberry.TelldusLive

using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Repositories;

var client = new TelldusLiveClient(
    consumerKey:       Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY")!,
    consumerKeySecret: Environment.GetEnvironmentVariable("TELLDUS_CONSUMER_KEY_SECRET")!,
    accessToken:       Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN")!,
    accessTokenSecret: Environment.GetEnvironmentVariable("TELLDUS_ACCESS_TOKEN_SECRET")!);

var deviceId = "12345"; // replace with your device ID

// Power
await client.Devices.TurnOnAsync(deviceId);
await client.Devices.TurnOffAsync(deviceId);

// Dim — level 0 (off) to 255 (full brightness)
await client.Devices.DimAsync(deviceId, level: 128);

// RGB color (for color bulbs)
await client.Devices.SetRgbAsync(deviceId, red: 255, green: 140, blue: 0);

// Blinds / shutters
await client.Devices.UpAsync(deviceId);
await client.Devices.DownAsync(deviceId);
await client.Devices.StopAsync(deviceId);

// Error handling
try
{
    await client.Devices.TurnOnAsync("invalid-id");
}
catch (RepositoryException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
}
