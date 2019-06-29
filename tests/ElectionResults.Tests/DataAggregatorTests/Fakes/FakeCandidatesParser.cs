using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services.CsvProcessing;

namespace ElectionResults.Tests.DataAggregatorTests.Fakes
{
    public class FakeCandidatesParser : ICsvParser
    {
        public Task<Result> Parse(ElectionResultsData electionResultsData, string csvContent)
        {
            WasInvoked = true;
            electionResultsData.Candidates = new List<Candidate>();
            return Task.FromResult(Result.Ok());
        }

        public bool WasInvoked { get; set; }
    }
}