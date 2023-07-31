namespace Sanatorium.Infrastructure.Topics;

public class TopicDeserializeException : Exception
{
    public TopicDeserializeException()
    {
    }

    public TopicDeserializeException(string message)
        : base(message)
    {
    }

    public TopicDeserializeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}