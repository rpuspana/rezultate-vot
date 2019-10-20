using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Repositories;

namespace ElectionResults.Core.Services.BlobContainer
{
    public class BucketUploader : IBucketUploader
    {
        private readonly IBucketRepository _bucketRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IFileProcessor _fileProcessor;
        private static HttpClient _httpClient;

        public BucketUploader(IBucketRepository bucketRepository, IFileRepository fileRepository, IFileProcessor fileProcessor)
        {
            _bucketRepository = bucketRepository;
            _fileRepository = fileRepository;
            _fileProcessor = fileProcessor;
            _httpClient = new HttpClient();
        }

        public async Task UploadFromUrl(ElectionResultsFile file)
        {
            var stream = await DownloadFile(file.URL);
            await UploadFileToStorage(stream, file.Name);
        }

        private static async Task<Stream> DownloadFile(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return new MemoryStream(Encoding.UTF8.GetBytes(response));
        }

        private async Task UploadFileToStorage(Stream fileStream, string fileName)
        {
            var bucketName = "code4-presidential-2019";
            var bucketExists = await _bucketRepository.DoesS3BucketExist(bucketName);
            if (bucketExists == false)
            {
                var response = await _bucketRepository.CreateBucket(bucketName);
                if (response.IsFailure)
                {
                    Console.WriteLine(response.Error);
                    return;
                }
            }

            var fileData = new FileData{FileName = fileName};
            fileData.Stream = new MemoryStream();
            fileStream.CopyTo(fileData.Stream);
            var uploadResponse = await _fileRepository.UploadFiles(bucketName, fileData);
            if (uploadResponse.IsSuccess)
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                await _fileProcessor.ProcessStream(fileStream, fileName);
            }
        }
    }
}