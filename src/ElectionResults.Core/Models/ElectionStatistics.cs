using CoreHelpers.WindowsAzure.Storage.Table.Attributes;

namespace ElectionResults.Core.Models
{
    public class ElectionStatistics
    {
        [PartitionKey]
        public string Id { get; set; }

        public string StatisticsJson { get; set; }

        [RowKey]
        public string Location { get; set; }

        public long FileTimestamp { get; set; }

        public string Type { get; set; }
    }
}
