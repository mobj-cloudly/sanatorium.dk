namespace Sanatorium.Core.Topics;

public class SubtopicExistsException : Exception
{
    public SubtopicExistsException()
    {
    }

    public SubtopicExistsException(string message)
        : base(message)
    {
    }

    public SubtopicExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}