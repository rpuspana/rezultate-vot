using System.Threading.Tasks;
using ElectionResults.Core.Services;
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
        public async Task Run([TimerTrigger("%ScheduleTriggerTime%")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            FunctionSettings.Initialize(context);
            await _csvDownloaderJob.DownloadFilesToBlobStorage();
        }
    }
}
