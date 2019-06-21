using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services
{
    public interface IResultsSource
    {
        Task<List<ElectionResultsFile>> GetListOfFilesWithElectionResults();
    }
}
