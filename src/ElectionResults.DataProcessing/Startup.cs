using ElectionResults.Core.Services;
using ElectionResults.Core.Storage;
using ElectionResults.DataProcessing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace ElectionResults.DataProcessing
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddTransient<ICsvDownloaderJob, CsvDownloaderJob>();
            builder.Services.AddTransient<IBlobUploader, BlobUploader>();
            builder.Services.AddTransient<IElectionConfigurationSource, ElectionConfigurationSource>();
            builder.Services.AddTransient<IResultsRepository, ResultsRepository>();
        }
    }
}
