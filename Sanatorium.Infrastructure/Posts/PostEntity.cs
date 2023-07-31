using Azure;
using Azure.Data.Tables;

namespace Sanatorium.Infrastructure.Posts;

public class PostEntity: ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string TitleEncoded { get; set; }
    public string MainTopic { get; set; }
    public string MainTopicEncoded { get; set; }
    public string Subtopic { get; set; }
    public string AuthorId { get; set; }
    public int ReadTimeMinutes { get; set; }
    public string TagsJson { get; set; }
    public string ImageUrl { get; set; }
    public string BodyJson { get; set; }
}