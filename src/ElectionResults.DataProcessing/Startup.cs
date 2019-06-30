using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Services.BlobContainer;
using ElectionResults.Core.Services.CsvDownload;
using ElectionResults.Core.Services.CsvProcessing;
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
            builder.Services.AddTransient<IBlobProcessor, BlobProcessor>();
            builder.Services.AddTransient<IStatisticsAggregator, StatisticsAggregator>();
        }
    }
}
