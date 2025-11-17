[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=bugs)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=wolfberry-ab_telldus-live-dotnet&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=wolfberry-ab_telldus-live-dotnet)

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
using Newtonsoft.Json;
using Wolfberry.TelldusLive;

namespace ConsoleAppDotNet6;

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
        var telldusClient = new TelldusLiveClient(
            consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

        // Example of getting clients (e.g. a Telldus Tellstick Znet Lite v2 controller)
        var clients = await telldusClient.Clients.GetClientsAsync();
        // Print out response in JSON format
        Console.WriteLine(JsonConvert.SerializeObject(clients));
    }
}
```

### Getting started with Telldus products

- [Telldus Guides & concepts](https://start.telldus.com/help/guides)

- [Telldus Manuals](https://start.telldus.com/help/manuals)

## Issues

Please, report any issues in the Issues tab.

### Known Issues

Especially the Events methods are limited tested.

## Contribute

Your contribution is very welcome.

## Third-Party licenses

This library depends on Newtonsoft.Json and TinyOAuth1. See the _third-party-licenses folder ind the repo root.

# Tags

Internet Of Things (IoT), Homeautomation, Home Automation, smart home, domotics, Telldus, SmartHome, Microsoft, DotNet, csharp, NuGet, MSIoT, Innovation, DotNetCore, SmartaHem, IIoT, Ligting
