using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services
{
    public interface IElectionConfigurationSource
    {
        Task<List<ElectionResultsFile>> GetListOfFilesWithElectionResults();

        Task<List<Candidate>> GetListOfCandidates();
    }
}
