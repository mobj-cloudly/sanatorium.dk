namespace Sanatorium.Core.Posts;

public interface IPostRepository
{
    public Task CreatePost(Post post);
    public Task<IEnumerable<Post>> ReadPosts();
    public Task UpdatePost(Post post);
    public Task DeletePost(Post post);
}