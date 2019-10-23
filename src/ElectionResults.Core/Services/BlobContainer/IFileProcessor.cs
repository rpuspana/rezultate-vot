using System.IO;
using System.Threading.Tasks;

namespace ElectionResults.Core.Services.BlobContainer
{
    public interface IFileProcessor
    {
        Task ProcessStream(Stream stream, string fileName);
    }
}