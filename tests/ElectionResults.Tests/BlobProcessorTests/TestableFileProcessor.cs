using System.IO;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Services.BlobContainer;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Core.Storage;

namespace ElectionResults.Tests.BlobProcessorTests
{
    public class TestableFileProcessor: FileProcessor
    {
        public TestableFileProcessor(IResultsRepository resultsRepository, IElectionConfigurationSource electionConfigurationSource, IStatisticsAggregator statisticsAggregator) : base(resultsRepository, electionConfigurationSource, statisticsAggregator)
        {
        }

        protected override Task<string> ReadCsvContent(Stream csvStream)
        {
            CsvWasReadAsString = true;
            return Task.FromResult("");
        }

        public bool CsvWasReadAsString { get; set; }
    }
}