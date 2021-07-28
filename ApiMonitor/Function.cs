using System.Net;
using System.Threading.Tasks;
using ApiTest;
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

namespace ApiMonitor
{
    public static class Function
    {
        [FunctionName("telldusEndpoints")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Telldus" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "resource", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "E.g.: sensors or devices")]
        [OpenApiParameter(name: "resourceParameter", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "E.g.: list")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string resource = req.Query["resource"];
            string resourceParameter = req.Query["resourceParameter"];

            log.LogInformation($"resource {resource}, parameter {resourceParameter}");
            
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
            return await TelldusApiRouter.Dispatch(client, resource, resourceParameter);
        }
    }
}

