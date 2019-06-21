using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services
{
    public class ResultsSource : IResultsSource
    {
        public Task<List<ElectionResultsFile>> GetListOfFilesWithElectionResults()
        {
            //TODO: This list will be retrieved from a configuration source
            return Task.FromResult(new List<ElectionResultsFile>
            {
                new ElectionResultsFile{ Id = "PART_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_PART.csv"},
                new ElectionResultsFile{ Id = "PART_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_PART.csv"},
                new ElectionResultsFile{ Id = "FINAL_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_FINAL.csv"},
                new ElectionResultsFile{ Id = "FINAL_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_FINAL.csv"},
                new ElectionResultsFile{ Id = "PROV_RO", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_RO_EUP_PROV.csv"},
                new ElectionResultsFile{ Id = "PROV_DSPR", URL = "https://prezenta.bec.ro/europarlamentare26052019/data/pv/csv/pv_SR_EUP_PROV.csv"}
            });
        }
    }
}