using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;
using ElectionResults.Core.Services.CsvProcessing;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ElectionResults.DataProcessing
{
    public class ResultsProcessor
    {
        public ResultsProcessor()
        {

        }

        [FunctionName("ResultsProcessor")]
        public static async Task Run([BlobTrigger("%BlobContainerName%/{name}", Connection = "")]Stream csvStream, string name, ILogger log, ExecutionContext context)
        {
            FunctionSettings.Initialize(context);

            var csvContent = await ReadCsvContent(csvStream);
            var dataAggregator = new DataAggregator(new List<ICsvParser>
            {
                new CandidatesResultsParser()
            });

            var aggregationResult = await dataAggregator.RetrieveElectionData(csvContent);

            if (aggregationResult.IsSuccess)
                await SaveJsonInDatabase(aggregationResult.Value, name);
        }

        private static async Task SaveJsonInDatabase(ElectionResultsData electionResultsData, string csvName)
        {
            
        }

        private static async Task<string> ReadCsvContent(Stream csvStream)
        {
            var buffer = new byte[csvStream.Length];
            await csvStream.ReadAsync(buffer, 0, (int)csvStream.Length);
            var csvContent = Encoding.UTF8.GetString(buffer);
            return csvContent;
        }
    }
}
