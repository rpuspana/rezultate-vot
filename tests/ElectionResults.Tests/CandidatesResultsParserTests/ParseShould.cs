using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using FluentAssertions;
using Xunit;

namespace ElectionResults.Tests.CandidatesResultsParserTests
{
    public class ParseShould
    {
        [Fact]
        public async Task not_return_null_list_of_candidates()
        {
            var candidatesResultsParser = new TestableCandidatesResultsParser();
            candidatesResultsParser.ParsedCandidates = new List<Candidate>();

            var result = await candidatesResultsParser.Parse(null, "");

            result.Value.Candidates.Should().NotBeNull();
        }

        [Theory]
        [InlineData(20, 20, 60, 60, 20, 20)]
        [InlineData(30, 33.33, 30, 33.33, 30, 33.33)]
        [InlineData(1, 1, 37, 37, 62, 62)]
        public async Task set_percentages_for_each_candidate(int c1Votes, decimal c1Percentage,
            int c2Votes, decimal c2Percentage,
            int c3Votes, decimal c3Percentage)
        {
            var candidatesResultsParser = new TestableCandidatesResultsParser();
            candidatesResultsParser.ParsedCandidates = CreateListOfCandidatesWithVotes(c1Votes, c2Votes, c3Votes);
            var electionResultsData = new ElectionResultsData();

            await candidatesResultsParser.Parse(electionResultsData, "");

            electionResultsData.Candidates[0].Percentage.Should().Be(c1Percentage);
            electionResultsData.Candidates[1].Percentage.Should().Be(c2Percentage);
            electionResultsData.Candidates[2].Percentage.Should().Be(c3Percentage);
        }

        private static List<Candidate> CreateListOfCandidatesWithVotes(int c1Votes, int c2Votes, int c3Votes)
        {
            return new List<Candidate>
            {
                new Candidate
                {
                    Name = "Candidate1",
                    Votes = c1Votes
                },
                new Candidate
                {
                    Name = "Candidate2",
                    Votes = c2Votes
                },
                new Candidate
                {
                    Name = "Candidate3",
                    Votes = c3Votes
                }
            };
        }
    }
}
