using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreHelpers.WindowsAzure.Storage.Table;
using CoreHelpers.WindowsAzure.Storage.Table.Models;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Models;
using Microsoft.Extensions.Options;

namespace ElectionResults.Core.Storage
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly string _azureConnectionString;

        public ResultsRepository(IOptions<AppConfig> config)
        {
            _azureConnectionString = config.Value?.AzureWebJobsStorage ?? FunctionSettings.AzureStorageConnectionString;
        }

        public async Task InsertResults(ElectionStatistics electionStatistics)
        {
            using (var storageContext = new StorageContext(_azureConnectionString))
            {
                await CreateResultsTable(storageContext);
                await storageContext.MergeOrInsertAsync(electionStatistics);
            }
        }

        public async Task<ElectionStatistics> GetLatestResults(string location, string type)
        {
            using (var storageContext = new StorageContext(_azureConnectionString))
            {
                await CreateResultsTable(storageContext);
                var statistics = await storageContext.QueryAsync<ElectionStatistics>(location, new List<QueryFilter>
                {
                    new QueryFilter{Operator = QueryFilterOperator.Equal, Property = "Type", Value = type}
                }, 1);
                return statistics.FirstOrDefault();
            }
        }

        private async Task CreateResultsTable(StorageContext storageContext)
        {
            storageContext.EnableAutoCreateTable();
            storageContext.AddAttributeMapper();
            storageContext.AddEntityMapper(typeof(ElectionStatistics), new DynamicTableEntityMapper
            {
                PartitionKeyFormat = "Location",
                RowKeyFormat = "Id",
                TableName = "ElectionStatistics"
            });
            await storageContext.CreateTableAsync(typeof(ElectionStatistics));
        }
    }
}