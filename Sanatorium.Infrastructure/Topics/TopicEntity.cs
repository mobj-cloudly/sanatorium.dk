using Azure;
using Azure.Data.Tables;

namespace Sanatorium.Infrastructure.Topics;

public class TopicEntity: ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Id { get; set; }
    public string MainTopic { get; set; }
    public string SubTopicsJson { get; set; }
}