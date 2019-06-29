using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Core.Storage;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ElectionResults.DataProcessing
{
    public class ResultsProcessor
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly IElectionConfigurationSource _electionConfigurationSource;

        public ResultsProcessor(IResultsRepository resultsRepository, IElectionConfigurationSource electionConfigurationSource)
        {
            _resultsRepository = resultsRepository;
            _electionConfigurationSource = electionConfigurationSource;
        }

        [FunctionName("ResultsProcessor")]
        public async Task Run([BlobTrigger("%BlobContainerName%/{name}", Connection = "")]Stream csvStream, string name, ILogger log, ExecutionContext context)
        {
            try
            {
                Config.Candidates = await _electionConfigurationSource.GetListOfCandidates();

                FunctionSettings.Initialize(context);

                var csvContent = await ReadCsvContent(csvStream);
                var dataAggregator = new DataAggregator(new List<ICsvParser>
                {
                    new CandidatesResultsParser()
                });

                var aggregationResult = await dataAggregator.RetrieveElectionData(csvContent);
            
                if (aggregationResult.IsSuccess)
                    await SaveResultsInDatabase(aggregationResult.Value, name);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
        }

        private async Task SaveResultsInDatabase(ElectionResultsData electionResultsData, string csvName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(csvName);
            var attributes = nameWithoutExtension.Split("_");
            var type = attributes[0];
            var location = attributes[1];
            var timestamp = attributes[2];
            var fileParsingResult = new FileParsingResult
            {
                Id = nameWithoutExtension,
                Type = type,
                Location = location,
                Timestamp = long.Parse(timestamp),
                ProcessedData = JsonConvert.SerializeObject(electionResultsData)
            };
            await _resultsRepository.InsertResults(fileParsingResult);
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
