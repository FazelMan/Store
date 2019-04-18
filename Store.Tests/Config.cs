using Microsoft.Extensions.Configuration;

namespace Store.Tests
{
    public  class Config
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();
            return config;
        }
    }
}