# Telldus Live .NET Library

This is a library for [Telldus Live API](https://api.telldus.com) with full feature support of the public API (180 methods!).
It allows you to manage clients (a.k.a. controllers), devices, events, groups, scheduler, sensors and your user accounts.

It's written in C# (targeting netstandard2.0) and is available as open-source and as NuGet. Since it can be used
in .NET Core it can be run from any platform where .NET Core runtime can execute (Windows, Mac & Linux).

The library fits well in command line applications, back-ends, web apps, ...

## Getting started

- Create an account and setup your system at: https://live.telldus.com/default/index
- Create credentials at https://api.telldus.com/.
- Install the [Wolfberry.TelldusLive NuGet package](https://www.nuget.org/packages/Wolfberry.TelldusLive/) into your project.
  - E.g.: `dotnet add package Wolfberry.TelldusLive`
- Example console application (Program.cs):
 ```c#
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;

namespace ConsoleAppDotNet31
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Get your keys and tokens from https://api.telldus.com/keys/index
            var consumerKey = "";
            var consumerKeySecret = "";
            var accessToken = "";
            var accessTokenSecret = "";

            // Setup Telldus Live client
            ITelldusLiveClient telldusClient =
                new TelldusLiveClient(
                    consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

            // Example of getting clients (e.g. a Telldus Tellstick Znet Lite v2 controller)
            var clients = await telldusClient.Clients.GetClientsAsync();
            // Print out response in JSON format
            Console.WriteLine(JsonConvert.SerializeObject(clients));
        }
    }
}
```

## Issues

Please, report any issues in the Issues tab.

### Known Issues

Especially the Events methods are limited tested.

## Contribute

Your contribution is very welcome.

# Tags

Internet Of Things (IoT), Homeautomation, Home Automation, smart home, domotics, Telldus, SmartHome, Microsoft, DotNet, csharp, NuGet, MSIoT, Innovation, DotNetCore, SmartaHem, IIoT, Ligting
