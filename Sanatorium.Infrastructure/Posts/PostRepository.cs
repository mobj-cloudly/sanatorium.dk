using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Sanatorium.Core.Posts;

namespace Sanatorium.Infrastructure.Posts;

public class PostRepository: IPostRepository
{
    private readonly TableClient _client;

    public PostRepository(IConfiguration configuration)
    {
        _client = new TableClient(
            configuration["Azure:TableStorage:ConnectionString"],
            configuration["Azure:TableStorage:PostTableName"]);
        _client.CreateIfNotExists();
    }
    public async Task CreatePost(Post post)
    {
        try
        {
            await _client.AddEntityAsync(post.ToPostEntity());
        }
        catch (RequestFailedException e)
        {
            throw new PostDeserializeException();
        }
    }

    public async Task<IEnumerable<Post>> ReadPosts()
    {
        try
        {
            var result = new List<Post>();
            var queryResult =
                _client.QueryAsync<PostEntity>(postEntity => postEntity.PartitionKey.Equals(PostMapper.PartitionKey));
            await foreach (var postEntity in queryResult) result.Add(postEntity.ToPost());
            return result;
        }
        catch (RequestFailedException e)
        {
            //log
            throw;
        }
    }

    public Task UpdatePost(Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeletePost(Post post)
    {
        throw new NotImplementedException();
    }
}