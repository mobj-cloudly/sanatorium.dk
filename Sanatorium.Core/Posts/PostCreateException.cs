namespace Sanatorium.Core.Posts;

public class PostCreateException : Exception
{
    public PostCreateException()
    {
    }

    public PostCreateException(string message)
        : base(message)
    {
    }

    public PostCreateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}