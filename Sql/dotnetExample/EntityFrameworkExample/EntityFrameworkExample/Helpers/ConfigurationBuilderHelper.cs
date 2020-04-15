using Microsoft.Extensions.Configuration;

namespace EntityFrameworkExample.Helpers
{
    public static class ConfigurationBuilderHelper
    {
        public static IConfiguration Configuration
        {
            get
            {
                return new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }

        public static string GetDefaultConnectionString()
        {
            return Configuration.GetValue<string>("ConnectionString");
        }
    }
}
