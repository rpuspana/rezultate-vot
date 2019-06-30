using System.IO;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Services.BlobContainer;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Core.Storage;

namespace ElectionResults.Tests.BlobProcessorTests
{
    public class TestableBlobProcessor: BlobProcessor
    {
        public TestableBlobProcessor(IResultsRepository resultsRepository, IElectionConfigurationSource electionConfigurationSource, IDataAggregator dataAggregator) : base(resultsRepository, electionConfigurationSource, dataAggregator)
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