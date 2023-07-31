using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Sanatorium.Core.Authors;

namespace Sanatorium.Infrastructure.Authors;

public class AuthorRepository: IAuthorRepository
{
    private readonly TableClient _client;

    public AuthorRepository(IConfiguration configuration)
    {
        _client = new TableClient(
            configuration["Azure:TableStorage:ConnectionString"],
            configuration["Azure:TableStorage:AuthorTableName"]);
        _client.CreateIfNotExists();
    }


    public async Task CreateAuthor(Author author)
    {
        try
        {
            await _client.AddEntityAsync(author.ToAuthorEntity());
        }
        catch (RequestFailedException e)
        {
            throw new AuthorCreateException();
        }
    }

    public async Task<IEnumerable<Author>> ReadAuthors()
    {
        try
        {
            var result = new List<Author>();
            var queryResult =
                 _client.QueryAsync<AuthorEntity>(authorEntity => authorEntity.PartitionKey.Equals(AuthorMapper.PartitionKey));
            await foreach (var authorEntity in queryResult) result.Add(authorEntity.ToAuthor());
            return result;
        }
        catch (RequestFailedException e)
        {
            //log
            throw;
        }
    }

    public Task UpdateAuthor(Author author)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthor(Author author)
    {
        throw new NotImplementedException();
    }
}