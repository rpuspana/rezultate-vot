using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ElectionResults.Core.Repositories
{
    public interface IFileRepository
    {
        Task<Result> UploadFiles(string bucketName, FileData fileData);
    }
}
