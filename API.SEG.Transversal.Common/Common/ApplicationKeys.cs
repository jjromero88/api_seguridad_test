using Microsoft.Extensions.Configuration;

namespace API.SEG.Transversal.Common
{
    public static class ApplicationKeys
    {

        private static IConfigurationRoot _config;
        static ApplicationKeys()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        public static int authenticateAbsoluteExpiration => int.TryParse(_config["CacheConfiguration:Authenticate:absoluteExpiration"], out var value) ? value : 10;
        public static int authenticateSlidingExpiration => int.TryParse(_config["CacheConfiguration:Authenticate:slidingExpiration"], out var value) ? value : 5;
        public static int authorizeAbsoluteExpiration => int.TryParse(_config["CacheConfiguration:Authorize:absoluteExpiration"], out var value) ? value : 480;
        public static int authorizeSlidingExpiration => int.TryParse(_config["CacheConfiguration:Authorize:slidingExpiration"], out var value) ? value : 60;
    }
}
