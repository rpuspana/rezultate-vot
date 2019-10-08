using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsvHelper;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services.CsvProcessing
{
    public class CandidatesResultsParser : ICsvParser
    {
        protected List<Candidate> Candidates;

        public async Task<Result<ElectionResultsData>> Parse(ElectionResultsData electionResultsData, string csvContent)
        {
            if (electionResultsData == null)
                electionResultsData = new ElectionResultsData();
            Candidates = Config.Candidates;

            await PopulateCandidatesListWithVotes(csvContent);
            electionResultsData.Candidates = Candidates;
            var sumOfVotes = electionResultsData.Candidates.Sum(c => c.Votes);
            StatisticsAggregator.CalculatePercentagesForCandidates(electionResultsData, sumOfVotes);

            return Result.Ok(electionResultsData);
        }

        protected virtual async Task PopulateCandidatesListWithVotes(string csvContent)
        {
            var csvParser = new CsvParser(new StringReader(csvContent));
            var headers = (await csvParser.ReadAsync()).ToList();
            do
            {
                var rowValues = await csvParser.ReadAsync();
                if (rowValues == null)
                    break;
                foreach (var candidate in Candidates)
                {
                    var votes = int.Parse(rowValues[headers.IndexOf(candidate.Id)]);
                    candidate.Votes += votes;
                }
            } while (true);
        }
    }
}
