using System.Text.Json;
using Sanatorium.Core.Topics;

namespace Sanatorium.Infrastructure.Topics;

public static class TopicMapper
{
    public const string PartitionKey = "Topics";

    public static Topic ToTopic(this TopicEntity topicEntity) =>
        new ()
        {
            Id = topicEntity.Id,
            MainTopic = topicEntity.MainTopic,
            SubTopics = DeserializeSubtopics(topicEntity.SubTopicsJson)
        };

    private static IEnumerable<string> DeserializeSubtopics(string subtopicsJson) =>
        JsonSerializer.Deserialize<IEnumerable<string>>(subtopicsJson)?? throw new TopicDeserializeException();

    public static TopicEntity ToTopicEntity(this Topic topic) =>
        new()
        {
            PartitionKey = PartitionKey,
            RowKey = topic.Id,
            Id = topic.Id,
            MainTopic = topic.MainTopic,
            SubTopicsJson = SerializeSubtopics(topic.SubTopics)
        };

    private static string SerializeSubtopics(IEnumerable<string> subtopics) =>
        JsonSerializer.Serialize(subtopics);
}