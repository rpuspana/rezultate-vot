using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services.CsvProcessing
{
    public interface IStatisticsAggregator
    {
        Task<Result<ElectionResultsData>> RetrieveElectionData(string csvContent);

        List<ICsvParser> CsvParsers { get; set; }
    }

    public class StatisticsAggregator : IStatisticsAggregator
    {
        public List<ICsvParser> CsvParsers { get; set;  } = new List<ICsvParser>();

        public async Task<Result<ElectionResultsData>> RetrieveElectionData(string csvContent)
        {
            var electionResults = new ElectionResultsData();
            foreach (var csvParser in CsvParsers)
            {
                await csvParser.Parse(electionResults, csvContent);
            }

            return Result.Ok(electionResults);
        }
    }
}
