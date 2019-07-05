using System.IO;
using System.Threading.Tasks;

namespace ElectionResults.Core.Services.BlobContainer
{
    public interface IBlobProcessor
    {
        Task ProcessStream(Stream stream, string fileName);
    }
}