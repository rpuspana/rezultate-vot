using System.Threading.Tasks;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Services
{
    public class CsvDownloaderJob: ICsvDownloaderJob
    {
        private readonly IBlobUploader _blobUploader;
        private readonly IResultsSource _resultsSource;

        public CsvDownloaderJob(IBlobUploader blobUploader, IResultsSource resultsSource)
        {
            _blobUploader = blobUploader;
            _resultsSource = resultsSource;
        }

        public async Task DownloadFilesToBlobStorage()
        {
            var files = await _resultsSource.GetListOfFilesWithElectionResults();
            var timestamp = SystemTime.Now.ToUnixTimeSeconds();
            foreach (var file in files)
            {
                file.Name = $"{file.Id}_{timestamp}.csv";
                await _blobUploader.UploadFromUrl(file);
            }
        }
    }
}
