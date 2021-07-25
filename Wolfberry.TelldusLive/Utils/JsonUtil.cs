using Newtonsoft.Json;

namespace Wolfberry.TelldusLive.Utils
{
    public static class JsonUtil
    {
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
