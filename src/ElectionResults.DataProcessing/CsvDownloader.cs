using System;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Services.CsvDownload;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ElectionResults.DataProcessing
{
    public class CsvDownloader
    {
        private readonly ICsvDownloaderJob _csvDownloaderJob;

        public CsvDownloader(ICsvDownloaderJob csvDownloaderJob)
        {
            _csvDownloaderJob = csvDownloaderJob;
        }

        [FunctionName("CsvDownloader")]
        public async Task Run([TimerTrigger("%ScheduleTriggerTime%", RunOnStartup = true)]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            FunctionSettings.Initialize(context);
            log.LogInformation($"Executing at {DateTime.Now}");
            await _csvDownloaderJob.DownloadFilesToBlobStorage();
        }
    }
}
