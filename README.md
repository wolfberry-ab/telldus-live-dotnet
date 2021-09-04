# Telldus Live .NET Library

This is a library for [Telldus Live API](https://api.telldus.com) with full feature support of the public API.
It allows you to manage clients (smart home hub/gateway/controller), devices, events, groups, scheduler, sensors and your user accounts.

It's written in C# (targeting netstandard2.0) and is available as open-source and as NuGet. Since it can be used
in .NET Core it can be run from any platform where .NET Core runtime can execute (Windows, Mac & Linux).

The library fits well in command line applications, back-ends, web apps, ...

## Getting started

- Create an account and setup your system at: https://live.telldus.com/default/index
- Create credentials at https://api.telldus.com/.
- Install the NuGet package into your project.
- Example code:
 ```c#
 // Enter credentials from your Telldus Live API account
 var config = new TelldusOAuth1Configuration
            {
                AccessTokenUrl = "https://api.telldus.com/oauth/accessToken",
                AuthorizeTokenUrl = "https://api.telldus.com/oauth/authorize",
                RequestTokenUrl = "https://api.telldus.com/oauth/requestToken",
                ConsumerKey = "ASDFASDFASDFASDFASDFASDF",
                ConsumerKeySecret = "ASDFASDFASDFASDFASDFSADF",
                AccessToken = "3c3c3c3cd3d3d3d3dcd3d",
                AccessTokenSecret = "ac9ac79ac79a8c7a8c7a98c7"
            };

// Configure the HTTP Client used by the repositories
var authenticator = new Authenticator(config);
var client = new TelldusHttpClient(authenticator);

// Example of turning on a device (e.g. a wall socket)
var deviceRepository = new DeviceRepository(client);
var status = await deviceRepository.TurnOnAsync(onOffDeviceId);
```

## Issues

Please, report any issues in the Issues tab.

## Contribute

Your contribution is very welcome.

# Tags

Internet Of Things (IoT), Homeautomation, Home Automation, smart home, domotics,