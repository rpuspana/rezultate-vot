using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services.CsvProcessing
{
    public interface ICsvParser
    {
        Task<Result> Parse(ElectionResultsData electionResultsData, string csvContent);
    }
}