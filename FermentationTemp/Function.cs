using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FermentationSensors.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Authentication;
using Wolfberry.TelldusLive.Repositories;

namespace FermentationSensors
{
    public static class Function 
    {
        [FunctionName("FermentationSensors")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("api_key", 
            SecuritySchemeType.ApiKey, 
            Name = "code", 
            In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, 
            contentType: "text/plain", 
            bodyType: typeof(SensorsResponse), 
            Description = "Sensor response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, 
                "get", 
                Route = null)] HttpRequest req,
            ILogger log)
        {
            string code = req.Query["code"];

            if (!"brygg2021".Equals(code))
            {
                return new UnauthorizedResult();
            }

            log.LogInformation("Incoming request");

            var sensorIds = new string[]
            {
                "1545931809", "1545931814"
            };

            var config = new TelldusOAuth1Configuration
            {
                AccessTokenUrl = "https://api.telldus.com/oauth/accessToken",
                AuthorizeTokenUrl = "https://api.telldus.com/oauth/authorize",
                RequestTokenUrl = "https://api.telldus.com/oauth/requestToken",
                ConsumerKey = "FEHUVEW84RAFR5SP22RABURUPHAFRUNU",
                ConsumerKeySecret = "ZUXEVEGA9USTAZEWRETHAQUBUR69U6EF",
                AccessToken = "66731a835637f7ea1cbaab7a34be4fa305db0b53e",
                AccessTokenSecret = "01008b2537a3e30b99998e927f488565"
            };

            IAuthenticator authenticator = new Authenticator(config);
            ITelldusHttpClient client = new TelldusHttpClient(authenticator);
            ISensorRepository sensorRepository = new SensorRepository(client);
            var sensorsResponse = await sensorRepository.GetSensorsAsync(true, true);
            var selectedSensors = sensorsResponse.Sensors
                .Where(x => sensorIds.Contains(x.Id))
                .ToList();

            log.LogInformation($"Returning {selectedSensors.Count} sensors");

            var response = new SensorsResponse
            {
                Sensors = selectedSensors
            };
            return new OkObjectResult(response);
        }
    }
}

