using Newtonsoft.Json.Linq;
using System;
using Wolfberry.TelldusLive.Utils;
using Wolfberry.TelldusLive.ViewModels;

namespace Wolfberry.TelldusLive.Repositories
{
    public static class ErrorParser
    {

        /// <summary>
        /// Try extract error message from JSON string.
        /// Empty or null strings are converted to ErrorResponse JSON strings.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>JSON string if error, otherwise null</returns>
        public static string GetOrCreateErrorMessage(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return JsonUtil.Serialize(new ErrorResponse
                {
                    Error = "Wolfberry.TelldusLive: Null or empty json string"
                });
            }

            var jObject = JObject.Parse(json);
            var found = jObject.TryGetValue("Error", 
                                                StringComparison.InvariantCultureIgnoreCase, 
                                                out var errorMessage);

            return found 
                ? errorMessage.ToString() 
                : null;
        }
    }
}
