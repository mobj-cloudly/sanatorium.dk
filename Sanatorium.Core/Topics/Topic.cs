namespace Sanatorium.Core.Topics;

public class Topic
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string MainTopic { get; set; } = string.Empty;
    public IEnumerable<string> SubTopics { get; set; } = Array.Empty<string>();
}