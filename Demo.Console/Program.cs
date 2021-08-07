using Wolfberry.TelldusLive;
using Wolfberry.TelldusLive.Authentication;

namespace Demo.Console
{
    public static class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Running...");
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

            var app = new App(client);
            app.Run().GetAwaiter().GetResult();
            System.Console.WriteLine("Done");
            System.Console.ReadLine();
        }
    }
}
    