namespace Sanatorium.Infrastructure.Posts;

public class PostDeserializeException : Exception
{
    public PostDeserializeException()
    {
    }

    public PostDeserializeException(string message)
        : base(message)
    {
    }

    public PostDeserializeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}