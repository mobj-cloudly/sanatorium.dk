using Sanatorium.Core.Authors;
using Sanatorium.Core.Posts;
using Sanatorium.Core.Topics;

namespace Sanatorium.Core.ContentManagement;

public interface IContentManager
{
    public Task ReloadAuthors();
    public Task ReloadPosts();
    public Task ReloadTopics();
    public Author GetAuthorById(string id);
    public Author GetAuthorByNameEncoded(string nameEncoded);
    public IEnumerable<Topic> GetTopics();
    public Post GetPost(string titleEncoded);
    public IEnumerable<Post> GetPostsByAuthor(Author author, SortCondition sortCondition = SortCondition.Newest, int maxCount = 0);
    public IEnumerable<Post> GetPosts(SortCondition sortCondition = SortCondition.Newest, int maxCount = 0);
    public IEnumerable<Post> GetPostsByTopic(Topic topic, SortCondition sortCondition = SortCondition.Newest, int maxCount = 0);
}