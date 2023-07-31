namespace Sanatorium.Core.Authors;

public interface IAuthorRepository
{
    public Task CreateAuthor(Author author);
    public Task<IEnumerable<Author>> ReadAuthors();
    public Task UpdateAuthor(Author author);
    public Task DeleteAuthor(Author author);
}