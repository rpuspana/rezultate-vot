using Microsoft.Extensions.Configuration;

namespace ElectionResults.Core.Services
{
    public static class FunctionSettings
    {
        private static IConfigurationRoot _config;

        public static void Initialize(Microsoft.Azure.WebJobs.ExecutionContext context)
        {
            var configurationBuilder = new ConfigurationBuilder();
            _config = configurationBuilder
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secrets.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string AzureStorageConnectionString => _config["AzureWebJobsStorage"];

        public static string BlobContainerName => _config["BlobContainerName"];

        public static string AzureTableName => _config["AzureTableName"];
    }
}
