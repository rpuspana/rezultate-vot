using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services.CsvProcessing;

namespace ElectionResults.Tests.DataAggregatorTests.Fakes
{
    public class FakePollingStationsParser : ICsvParser
    {
        public Task<Result> Parse(ElectionResultsData electionResultsData, string csvContent)
        {
            electionResultsData.PollingStations = new List<PollingStation>();
            return Task.FromResult(Result.Ok());
        }
    }
}