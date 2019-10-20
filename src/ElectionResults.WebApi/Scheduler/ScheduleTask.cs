using System;
using System.Threading.Tasks;
using ElectionResults.Core.Services.CsvDownload;
using Microsoft.Extensions.DependencyInjection;

namespace ElectionResults.WebApi.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ICsvDownloaderJob _csvDownloaderJob;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ICsvDownloaderJob csvDownloaderJob) : base(serviceScopeFactory)
        {
            _csvDownloaderJob = csvDownloaderJob;
        }

        protected override string Schedule => "*/5 * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            Console.WriteLine($"Processing starts here at {DateTime.UtcNow:F}");
            await _csvDownloaderJob.DownloadFilesToBlobStorage();
        }
    }
}