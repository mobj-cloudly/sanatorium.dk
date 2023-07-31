namespace Sanatorium.Core.Topics;

public interface ITopicRepository
{
    public Task CreateTopic(Topic topic);
    public Task<IEnumerable<Topic>> ReadTopics();
    public Task UpdateTopic();
}