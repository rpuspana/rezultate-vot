using System;
using System.Linq;
using System.Threading.Tasks;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;
using NSubstitute;
using Xunit;

namespace ElectionResults.Tests.CsvDownloaderJobTests
{
    public class DownloadFilesToBlobStorageShould
    {
        private IBlobUploader _blobUploader;
        private IElectionConfigurationSource _electionConfigurationSource;

        [Fact]
        public async Task RetrieveListOfCsvFiles()
        {
            var csvDownloaderJob = CreatecsvDownloaderJob();
            CreateResultsSourceMock(new ElectionResultsFile());

            await csvDownloaderJob.DownloadFilesToBlobStorage();

            await _electionConfigurationSource.Received(1).GetListOfFilesWithElectionResults();
        }

        [Fact]
        public async Task UploadFilesToBlobContainer()
        {
            var csvDownloaderJob = CreatecsvDownloaderJob();
            CreateResultsSourceMock( new ElectionResultsFile() );

            await csvDownloaderJob.DownloadFilesToBlobStorage();

            await _blobUploader
                .Received(1)
                .UploadFromUrl(Arg.Any<ElectionResultsFile>());
        }

        [Fact]
        public async Task UploadSameNumberOfFilesThatItReceived()
        {
            var csvDownloaderJob = CreatecsvDownloaderJob();
            CreateResultsSourceMock( new ElectionResultsFile(), new ElectionResultsFile() );

            await csvDownloaderJob.DownloadFilesToBlobStorage();

            await _blobUploader
                .Received(2)
                .UploadFromUrl(Arg.Any<ElectionResultsFile>());
        }

        [Fact]
        public async Task UseSameTimestampForEachFile()
        {
            var csvDownloaderJob = CreatecsvDownloaderJob();
            CreateResultsSourceMock( new ElectionResultsFile(), new ElectionResultsFile() );
            SystemTime.Now = DateTimeOffset.UtcNow;
            var timestamp = SystemTime.Now.ToUnixTimeSeconds();

            await csvDownloaderJob.DownloadFilesToBlobStorage();

            await _blobUploader
                .Received(2)
                .UploadFromUrl(Arg.Is<ElectionResultsFile>(f => f.Name.Contains(timestamp.ToString())));
        }

        [Fact]
        public async Task BuildNameOfUploadedFiles()
        {
            var csvDownloaderJob = CreatecsvDownloaderJob();
            CreateResultsSourceMock(new ElectionResultsFile { Id = "abc" });
            SystemTime.Now = DateTimeOffset.UtcNow;
            var timestamp = SystemTime.Now.ToUnixTimeSeconds();

            await csvDownloaderJob.DownloadFilesToBlobStorage();

            await _blobUploader
                .Received(1)
                .UploadFromUrl(Arg.Is<ElectionResultsFile>(f => f.Name == $"abc_{timestamp}.csv"));
        }

        private CsvDownloaderJob CreatecsvDownloaderJob()
        {
            _blobUploader = Substitute.For<IBlobUploader>();
            _electionConfigurationSource = Substitute.For<IElectionConfigurationSource>();
            return new CsvDownloaderJob(_blobUploader, _electionConfigurationSource);
        }

        private void CreateResultsSourceMock(params ElectionResultsFile[] files)
        {
            _electionConfigurationSource.GetListOfFilesWithElectionResults()
                .ReturnsForAnyArgs(info => Task.FromResult(files.ToList()));
        }
    }
}
