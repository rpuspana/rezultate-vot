using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Storage
{
    public interface IResultsRepository
    {
        Task InsertResults(FileParsingResult fileParsingResult);
    }
}
