using System.Threading.Tasks;
using CoreHelpers.WindowsAzure.Storage.Table;
using ElectionResults.Core.Infrastructure;
using ElectionResults.Core.Models;

namespace ElectionResults.Core.Storage
{
    public class ResultsRepository : IResultsRepository
    {
        public async Task InsertResults(ElectionStatistics electionStatistics)
        {
            using (var storageContext = new StorageContext(FunctionSettings.AzureStorageConnectionString))
            {
                storageContext.EnableAutoCreateTable();
                storageContext.AddAttributeMapper();
                storageContext.AddEntityMapper(typeof(ElectionStatistics), new DynamicTableEntityMapper
                {
                    PartitionKeyFormat = "Location",
                    RowKeyFormat = "Id",
                    TableName = FunctionSettings.AzureTableName
                });
                await storageContext.CreateTableAsync(typeof(ElectionStatistics));

                await storageContext.MergeOrInsertAsync(electionStatistics);
            }
        }
    }
}