using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Core.Storage;

namespace ElectionResults.Core.Services.BlobContainer
{
    public class BlobProcessor : IBlobProcessor
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly IElectionConfigurationSource _electionConfigurationSource;
        private readonly IDataAggregator _dataAggregator;

        public BlobProcessor(IResultsRepository resultsRepository,
            IElectionConfigurationSource electionConfigurationSource,
            IDataAggregator dataAggregator)
        {
            _resultsRepository = resultsRepository;
            _electionConfigurationSource = electionConfigurationSource;
            _dataAggregator = dataAggregator;
            _dataAggregator.CsvParsers = new List<ICsvParser>
            {
                new CandidatesResultsParser()
            };
        }

        public async Task ProcessStream(Stream csvStream, string fileName)
        {
            Config.Candidates = await _electionConfigurationSource.GetListOfCandidates();
            var csvContent = await ReadCsvContent(csvStream);
            var aggregationResult = await _dataAggregator.RetrieveElectionData(csvContent);
            if (aggregationResult.IsSuccess)
            {
                var electionStatistics = FileNameParser.BuildElectionStatistics(fileName, aggregationResult.Value);
                await _resultsRepository.InsertResults(electionStatistics);
            }
        }

        protected virtual async Task<string> ReadCsvContent(Stream csvStream)
        {
            var buffer = new byte[csvStream.Length];
            await csvStream.ReadAsync(buffer, 0, (int)csvStream.Length);
            var csvContent = Encoding.UTF8.GetString(buffer);
            return csvContent;
        }
    }
}
