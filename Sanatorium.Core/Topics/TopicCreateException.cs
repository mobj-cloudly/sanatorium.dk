namespace Sanatorium.Core.Topics;

public class TopicCreateException : Exception
{
    public TopicCreateException()
    {
    }

    public TopicCreateException(string message)
        : base(message)
    {
    }

    public TopicCreateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}