using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ElectionResults.DataProcessing
{
    public class CsvDownloader
    {
        private readonly IBlobUploader _blobUploader;

        public CsvDownloader(IBlobUploader blobUploader)
        {
            _blobUploader = blobUploader;
        }

        [FunctionName("CsvDownloader")]
        public async Task Run([TimerTrigger("%ScheduleTriggerTime%", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            //TODO: This list will be retrieved from a configuration source
            var results = new List<ElectionResultsFile>
            {
                new ElectionResultsFile{ Name = "PART_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_PART.csv"},
                new ElectionResultsFile{ Name = "PART_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_PART.csv"},
                new ElectionResultsFile{ Name = "FINAL_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_FINAL.csv"},
                new ElectionResultsFile{ Name = "FINAL_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_FINAL.csv"},
                new ElectionResultsFile{ Name = "PROV_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_PROV.csv"},
                new ElectionResultsFile{ Name = "PROV_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_PROV.csv"}
            };

            foreach (var resultsFile in results)
            {
                await _blobUploader.UploadFromUrl(resultsFile);
            }
        }
    }
}
