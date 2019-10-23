using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Repositories
{
    public interface IBucketRepository
    {
        Task<bool> DoesS3BucketExist(string bucketName);

        Task<Result<CreateBucketResponse>> CreateBucket(string bucketName);
    }
}
