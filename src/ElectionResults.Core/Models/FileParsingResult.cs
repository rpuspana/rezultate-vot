using CoreHelpers.WindowsAzure.Storage.Table.Attributes;

namespace ElectionResults.Core.Models
{
    public class FileParsingResult
    {
        [RowKey]
        [PartitionKey]
        public string Id { get; set; }

        public string ProcessedData { get; set; }

        public string Location { get; set; }

        public long Timestamp { get; set; }
        public string Type { get; set; }
    }
}
