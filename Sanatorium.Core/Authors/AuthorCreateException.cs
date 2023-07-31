namespace Sanatorium.Core.Authors;

public class AuthorCreateException : Exception
{
    public AuthorCreateException()
    {
    }

    public AuthorCreateException(string message)
        : base(message)
    {
    }

    public AuthorCreateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}