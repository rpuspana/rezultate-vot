using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Storage
{
    public interface IResultsRepository
    {
        Task InsertResults(ElectionStatistics electionStatistics);
    }
}
