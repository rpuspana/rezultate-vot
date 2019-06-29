using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services.CsvProcessing
{
    public interface IDataAggregator
    {
        Task<Result<ElectionResultsData>> RetrieveElectionData(string csvContent);
    }

    public class DataAggregator : IDataAggregator
    {
        private readonly List<ICsvParser> _csvParsers;

        public DataAggregator(List<ICsvParser> csvParsers)
        {
            _csvParsers = csvParsers;
        }

        public async Task<Result<ElectionResultsData>> RetrieveElectionData(string csvContent)
        {
            var electionResults = new ElectionResultsData();
            foreach (var csvParser in _csvParsers)
            {
                await csvParser.Parse(electionResults, csvContent);
            }

            return Result.Ok(electionResults);
        }
    }
}
