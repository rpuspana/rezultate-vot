using System.Threading.Tasks;
using CoreHelpers.WindowsAzure.Storage.Table;
using ElectionResults.Core.Models;
using ElectionResults.Core.Services;

namespace ElectionResults.Core.Storage
{
    public class ResultsRepository : IResultsRepository
    {
        public async Task InsertResults(FileParsingResult fileParsingResult)
        {
            using (var storageContext = new StorageContext(FunctionSettings.AzureStorageConnectionString))
            {
                storageContext.EnableAutoCreateTable();
                storageContext.AddAttributeMapper();
                storageContext.AddEntityMapper(typeof(FileParsingResult), new DynamicTableEntityMapper
                {
                    PartitionKeyFormat = "Id",
                    RowKeyFormat = "Id",
                    TableName = FunctionSettings.AzureTableName
                });
                await storageContext.CreateTableAsync(typeof(FileParsingResult), true);

                await storageContext.MergeOrInsertAsync(fileParsingResult);
            }
        }
    }
}