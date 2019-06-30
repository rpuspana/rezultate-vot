using System.IO;
using ElectionResults.Core.Models;
using Newtonsoft.Json;

namespace ElectionResults.Core.Services
{
    public class FileNameParser
    {
        public static ElectionStatistics BuildElectionStatistics(string fileName, ElectionResultsData electionResultsData)
        {
            var electionStatistics = new ElectionStatistics();
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            electionStatistics.Id = fileNameWithoutExtension;
            var attributes = fileNameWithoutExtension.Split('_');
            electionStatistics.Location = attributes[1];
            electionStatistics.Type = attributes[0];
            electionStatistics.FileTimestamp = long.Parse(attributes[2]);
            electionStatistics.StatisticsJson = JsonConvert.SerializeObject(electionResultsData);
            return electionStatistics;
        }
    }
}
