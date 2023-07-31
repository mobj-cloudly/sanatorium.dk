using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Sanatorium.Core.Topics;
using static System.Net.WebUtility;

namespace Sanatorium.Infrastructure.Topics;

public class TopicRepository: ITopicRepository
{
    private readonly TableClient _client;

    public TopicRepository(IConfiguration configuration)
    {
        _client = new TableClient(
            configuration["Azure:TableStorage:ConnectionString"],
            configuration["Azure:TableStorage:TopicTableName"]);
        _client.CreateIfNotExists();
    }

    public async Task CreateTopic(Topic topic)
    {
        try
        {
            var existingTopics = await ReadTopics();
            var topicNameEncoded = UrlEncode(topic.MainTopic);
            if (existingTopics.Any(t => t.MainTopic.ToLower().Equals(topicNameEncoded.ToLower())))
                throw new TopicCreateException("Topic exists");
            await _client.AddEntityAsync(topic.ToTopicEntity());
        }
        catch (RequestFailedException e)
        {
            throw new TopicCreateException();
        }
    }

    public async Task<IEnumerable<Topic>> ReadTopics()
    {
        try
        {
            var result = new List<Topic>();
            var queryResult =
                _client.QueryAsync<TopicEntity>(topicEntity => topicEntity.PartitionKey.Equals(TopicMapper.PartitionKey));
            await foreach (var topicEntity in queryResult) result.Add(topicEntity.ToTopic());
            return result;
        }
        catch (RequestFailedException e)
        {
            //log
            throw;
        }
    }

    public Task UpdateTopic()
    {
        //cannot change topic name. throw SubtopExistException if subtopic exists.
        throw new NotImplementedException();
    }
}