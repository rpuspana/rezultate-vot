using System;
using System.Threading.Tasks;
using ElectionResults.Core.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ElectionResults.DataProcessing
{
    public class CsvDownloader
    {
        private readonly IBlobUploader _blobUploader;
        private readonly IResultsSource _resultsSource;

        public CsvDownloader(IBlobUploader blobUploader, IResultsSource resultsSource)
        {
            _blobUploader = blobUploader;
            _resultsSource = resultsSource;
        }

        [FunctionName("CsvDownloader")]
        public async Task Run([TimerTrigger("%ScheduleTriggerTime%")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            FunctionSettings.Initialize(context);
            var results = await _resultsSource.GetListOfFilesWithElectionResults();

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var resultsFile in results)
            {
                var fileName = $"{resultsFile.Id}_{timestamp}_.csv";
                resultsFile.Name = fileName;
                await _blobUploader.UploadFromUrl(resultsFile);
            }
        }
    }
}
