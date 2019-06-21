using System.Threading.Tasks;

namespace ElectionResults.Core.Services
{
    public interface ICsvDownloaderJob
    {
        Task DownloadFilesToBlobStorage();
    }
}