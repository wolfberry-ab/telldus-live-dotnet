using Newtonsoft.Json.Linq;
using System;
using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Utils;

namespace Wolfberry.TelldusLive.Repositories
{
    public static class ErrorParser
    {

        /// <summary>
        /// Try extract error message from JSON string.
        /// Empty or null strings are converted to ErrorResponse JSON strings.
        /// </summary>
        /// <param name="json">JSON text string</param>
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

            // Can be also be a non-valid JSON e.g. "error code: 1001" (DNS resolution error)
            if (json.StartsWith("error", StringComparison.InvariantCultureIgnoreCase))
            {
                return JsonUtil.Serialize(new ErrorResponse
                {
                    Error = json
                });
            }

            JObject jObject;

            try
            {
                // Will fail if not an object (error responses are objects)
                // If the json starts with array it will cause a read exception
                jObject = JObject.Parse(json);
            }
            catch (Exception)
            {
                return null;
            }

            var found = jObject.TryGetValue("Error",
                    StringComparison.InvariantCultureIgnoreCase,
                    out var errorMessage);

            return found
                ? errorMessage.ToString()
                : null;
        }
    }
}
