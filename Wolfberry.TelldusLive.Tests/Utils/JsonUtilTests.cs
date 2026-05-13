using Wolfberry.TelldusLive.Models;
using Wolfberry.TelldusLive.Utils;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Utils
{
    public class JsonUtilTests
    {
        [Fact]
        public void Serialize_Object_ReturnsJsonString()
        {
            var response = new StatusResponse { Status = "success" };

            var json = JsonUtil.Serialize(response);

            Assert.NotNull(json);
            Assert.Contains("success", json);
        }

        [Fact]
        public void Deserialize_ValidJson_ReturnsObject()
        {
            const string json = "{\"Status\":\"success\"}";

            var result = JsonUtil.Deserialize<StatusResponse>(json);

            Assert.NotNull(result);
            Assert.Equal("success", result.Status);
        }
    }
}
