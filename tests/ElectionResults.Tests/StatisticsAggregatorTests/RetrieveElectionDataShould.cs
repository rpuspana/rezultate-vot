using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Tests.StatisticsAggregatorTests.Fakes;
using FluentAssertions;
using Xunit;

namespace ElectionResults.Tests.StatisticsAggregatorTests
{
    public class RetrieveElectionDataShould
    {
        [Fact]
        public async Task apply_all_defined_aggregations()
        {
            var firstParser = new FakeCandidatesParser();
            var secondParser = new FakeCandidatesParser();
            var csvParsers = new List<ICsvParser>
            {
                firstParser,
                secondParser
            };
            var statisticsAggregator = new StatisticsAggregator { CsvParsers = csvParsers };
            await statisticsAggregator.RetrieveElectionData("");

            firstParser.WasInvoked.Should().BeTrue();
            secondParser.WasInvoked.Should().BeTrue();
        }

        [Fact]
        public async Task build_election_results_model_with_results_from_each_parser()
        {
            var csvParsers = new List<ICsvParser>
            {
                new FakeCandidatesParser(),
                new FakePollingStationsParser()
            };
            var statisticsAggregator = new StatisticsAggregator { CsvParsers = csvParsers };

            var aggregationResult = await statisticsAggregator.RetrieveElectionData("");

            aggregationResult.Value.Candidates.Should().NotBeNull();
            aggregationResult.Value.PollingStations.Should().NotBeNull();
        }
    }
}
