using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Models;
using Microsoft.WindowsAzure.Storage;

namespace ElectionResults.Core.Services.BlobContainer
{
    public class BlobUploader : IBlobUploader
    {
        private static HttpClient _httpClient;

        public BlobUploader()
        {
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

        private static async Task UploadFileToStorage(Stream fileStream, string fileName)
        {
            var connectionString = FunctionSettings.AzureStorageConnectionString;
            var containerName = FunctionSettings.BlobContainerName;

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            var blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = "text/csv";
            await blockBlob.UploadFromStreamAsync(fileStream);
        }
    }
}