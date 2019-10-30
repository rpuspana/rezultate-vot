using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;
using ElectionResults.Core.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElectionResults.WebApi.Controllers
{
    [Route("api/results")]
    public class ResultsController : Controller
    {
        private readonly IResultsAggregator _resultsAggregator;
        private readonly IOptions<AppConfig> _config;

        public ResultsController(IResultsAggregator resultsAggregator, IOptions<AppConfig> config)
        {
            _resultsAggregator = resultsAggregator;
            _config = config;
        }

        [HttpGet("")]
        public async Task<ElectionResultsData> GetLatestResults([FromQuery] ResultsType type)
        {
            return await _resultsAggregator.GetResults(type);
        }
    }
}
