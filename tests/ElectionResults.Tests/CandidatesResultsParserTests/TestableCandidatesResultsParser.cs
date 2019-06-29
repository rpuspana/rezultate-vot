using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services.CsvProcessing;

namespace ElectionResults.Tests.CandidatesResultsParserTests
{
    public class TestableCandidatesResultsParser: CandidatesResultsParser
    {
        protected override Task PopulateCandidatesListWithVotes(string csvContent)
        {
            Candidates = ParsedCandidates;
            return Task.CompletedTask;
        }

        public List<Candidate> ParsedCandidates { get; set; }
    }
}