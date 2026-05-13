using Wolfberry.TelldusLive.Authentication;
using Xunit;

namespace Wolfberry.TelldusLive.Tests.Authentication
{
    public class AuthenticatorTests
    {
        private static TelldusOAuth1Configuration CreateConfig()
        {
            return new TelldusOAuth1Configuration
            {
                ConsumerKey = "key",
                ConsumerKeySecret = "secret",
                AccessToken = "token",
                AccessTokenSecret = "tokensecret",
                AccessTokenUrl = "https://example.com/access",
                AuthorizeTokenUrl = "https://example.com/auth",
                RequestTokenUrl = "https://example.com/request"
            };
        }

        [Fact]
        public void Constructor_ValidConfig_CreatesInstance()
        {
            var config = CreateConfig();

            var authenticator = new Authenticator(config);

            Assert.NotNull(authenticator);
        }

        [Fact]
        public void InitializeHttpClient_SetsHttpClient()
        {
            var config = CreateConfig();
            var authenticator = new Authenticator(config);

            authenticator.InitializeHttpClient();

            Assert.NotNull(authenticator.HttpClient);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            var config = CreateConfig();
            var authenticator = new Authenticator(config);
            authenticator.InitializeHttpClient();

            var exception = Record.Exception(() => authenticator.Dispose());

            Assert.Null(exception);
        }

        [Fact]
        public void Dispose_WithNullHttpClient_DoesNotThrow()
        {
            var config = CreateConfig();
            var authenticator = new Authenticator(config);

            var exception = Record.Exception(() => authenticator.Dispose());

            Assert.Null(exception);
        }
    }
}
