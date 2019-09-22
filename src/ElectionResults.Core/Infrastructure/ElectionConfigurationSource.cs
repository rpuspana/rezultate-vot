using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Infrastructure
{
    public class ElectionConfigurationSource : IElectionConfigurationSource
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

        public Task<List<Candidate>> GetListOfCandidates()
        {
            //TODO: This list will be retrieved from a configuration source
            return Task.FromResult(new List<Candidate>
            {
                new Candidate
                {
                    Id = "g1",
                    Name = "PARTIDUL SOCIAL DEMOCRAT"
                },
                new Candidate
                {
                    Id = "g2",
                    Name = "ALIANȚA 2020 USR PLUS"
                },
                new Candidate
                {
                    Id = "g3",
                    Name = "PARTIDUL PRO ROMÂNIA"
                },
                new Candidate
                {
                    Id = "g4",
                    Name = "UNIUNEA DEMOCRATĂ MAGHIARĂ DIN ROMÂNIA"
                },
                new Candidate
                {
                    Id = "g5",
                    Name = "PARTIDUL NAȚIONAL LIBERAL"
                },
                new Candidate
                {
                    Id = "g6",
                    Name = "PARTIDUL ALIANȚA LIBERALILOR ȘI DEMOCRAȚILOR - ALDE"
                },
                new Candidate
                {
                    Id = "g7",
                    Name = "PARTIDUL PRODEMO"
                },
                new Candidate
                {
                    Id = "g8",
                    Name = "PARTIDUL MIȘCAREA POPULARĂ"
                },
                new Candidate
                {
                    Id = "g9",
                    Name = "PARTIDUL SOCIALIST ROMÂN"
                },
                new Candidate
                {
                    Id = "g10",
                    Name = "PARTIDUL SOCIAL DEMOCRAT INDEPENDENT"
                },
                new Candidate
                {
                    Id = "g11",
                    Name = "PARTIDUL ROMÂNIA UNITĂ"
                },
                new Candidate
                {
                    Id = "g12",
                    Name = "UNIUNEA NAȚIONALĂ PENTRU PROGRESUL ROMÂNIEI"
                },
                new Candidate
                {
                    Id = "g13",
                    Name = "BLOCUL UNITĂȚII NAȚIONALE - BUN"
                },
                new Candidate
                {
                    Id = "g14",
                    Name = "GREGORIANA-CARMEN TUDORAN"
                },
                new Candidate
                {
                    Id = "g15",
                    Name = "GEORGE-NICOLAE SIMION"
                },
                new Candidate
                {
                    Id = "g16",
                    Name = "PETER COSTEA"
                }
            });
        }
    }
}