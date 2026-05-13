[![CI Pipeline](https://github.com/wolfberry-ab/telldus-live-dotnet/actions/workflows/ci-pipeline.yml/badge.svg)](https://github.com/wolfberry-ab/telldus-live-dotnet/actions/workflows/ci-pipeline.yml)
[![NuGet](https://img.shields.io/nuget/v/Wolfberry.TelldusLive)](https://www.nuget.org/packages/Wolfberry.TelldusLive/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Wolfberry.TelldusLive)](https://www.nuget.org/packages/Wolfberry.TelldusLive/)
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

A .NET library for the [Telldus Live API](https://api.telldus.com) with full coverage of the public API (180 methods).
Manage clients (controllers), devices, events, groups, scheduler, sensors, and user accounts.

Targets `netstandard2.0` — compatible with .NET Framework 4.6.1+ and all .NET versions up to .NET 10. Runs on Windows, Mac, and Linux.

## Requirements

- .NET Framework 4.6.1+ or .NET Core 2.0+ (including .NET 5–10)
- A [Telldus Live](https://live.telldus.com/default/index) account with API credentials

## Getting started

1. Create an account at https://live.telldus.com/default/index
2. Generate API credentials at https://api.telldus.com/keys/index
3. Install the package:
   ```
   dotnet add package Wolfberry.TelldusLive
   ```
4. Use the client:
   ```csharp
   using Newtonsoft.Json;
   using Wolfberry.TelldusLive;

   var telldusClient = new TelldusLiveClient(
       consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

   // Get all controllers (e.g. Tellstick Znet Lite v2)
   var clients = await telldusClient.Clients.GetClientsAsync();
   Console.WriteLine(JsonConvert.SerializeObject(clients));
   ```

### Telldus product resources

- [Guides & concepts](https://start.telldus.com/help/guides)
- [Manuals](https://start.telldus.com/help/manuals)

## Issues

Please report issues in the [Issues tab](https://github.com/wolfberry-ab/telldus-live-dotnet/issues).

## Contribute

Contributions are very welcome.

## Third-party licenses

This library depends on Newtonsoft.Json and TinyOAuth1. See the `_third-party-licenses` folder in the repo root.
