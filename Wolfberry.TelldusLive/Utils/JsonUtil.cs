using Newtonsoft.Json;

namespace Wolfberry.TelldusLive.Utils
{
    /// <summary>
    /// Wrapper for JSON serialization and deserialization
    /// </summary>
    public static class JsonUtil
    {
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
