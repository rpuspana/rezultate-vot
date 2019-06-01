using System;
using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services
{
    public class BlobUploader : IBlobUploader
    {
        public async Task UploadFromUrl(ElectionResultsFile file)
        {
            Console.WriteLine($"Uploading file {file.Name}");
        }
    }
}