using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services.CsvProcessing;
using ElectionResults.Core.Storage;
using Newtonsoft.Json;

namespace ElectionResults.Core.Services
{
    public class ResultsAggregator : IResultsAggregator
    {
        private readonly IResultsRepository _resultsRepository;

        public ResultsAggregator(IResultsRepository resultsRepository)
        {
            _resultsRepository = resultsRepository;
        }
        public async Task<ElectionResultsData> GetResults(ResultsType type)
        {
            string resultsType = ConvertEnumToString(type);

            var localResults = await _resultsRepository.GetLatestResults(Consts.LOCAL, resultsType);
            var diasporaResults = await _resultsRepository.GetLatestResults(Consts.DIASPORA, resultsType);
            var localResultsData = JsonConvert.DeserializeObject<ElectionResultsData>(localResults.StatisticsJson);
            var diasporaResultsData = JsonConvert.DeserializeObject<ElectionResultsData>(diasporaResults.StatisticsJson);
            return StatisticsAggregator.CombineResults(localResultsData, diasporaResultsData);
        }

        private static string ConvertEnumToString(ResultsType type)
        {
            return type
                       .GetType()
                       .GetMember(type.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DescriptionAttribute>()
                       ?.Description ?? type.ToString();
        }
    }
}
