using Sanatorium.Core.Authors;
using Sanatorium.Core.Posts;
using Sanatorium.Core.Topics;

namespace Sanatorium.Core.ContentManagement;

public class ContentManager: IContentManager
{
    private readonly IAuthorRepository _authors;
    private readonly IPostRepository _posts;
    private readonly ITopicRepository _topics;
    private IEnumerable<Author> CachedAuthors { get; set; }
    private IEnumerable<Post> CachedPosts { get; set; }
    private IEnumerable<Topic> CachedTopics { get; set; }

    public ContentManager(IAuthorRepository authors, IPostRepository posts, ITopicRepository topics)
    {
        _authors = authors;
        _posts = posts;
        _topics = topics;
        CachedAuthors = Array.Empty<Author>();
        CachedPosts = Array.Empty<Post>();
        CachedTopics = Array.Empty<Topic>();
        LoadDataIntoCaches();
    }

    private void LoadDataIntoCaches()
    {
        var reloadAuthors = Task.Run(ReloadAuthors);
        var reloadPosts = Task.Run(ReloadPosts);
        var reloadTopics = Task.Run(ReloadTopics);
        Task.WhenAll(reloadAuthors, reloadPosts, reloadTopics).Wait();
    }

    public async Task ReloadAuthors() =>
        CachedAuthors = await _authors.ReadAuthors();

    public async Task ReloadPosts() =>
        CachedPosts = await _posts.ReadPosts(); //join viewcount statistics later

    public async Task ReloadTopics() =>
        CachedTopics = await _topics.ReadTopics();

    public Author GetAuthorById(string id) =>
        CachedAuthors.First(author => author.Id.Equals(id));

    public Author GetAuthorByNameEncoded(string nameEncoded) =>
        CachedAuthors.First(author => author.NameEncoded.Equals(nameEncoded));

    public IEnumerable<Topic> GetTopics() =>
        CachedTopics;

    public Post GetPost(string titleEncoded) =>
        CachedPosts.First(post => post.TitleEncoded.Equals(titleEncoded));

    public IEnumerable<Post> GetPostsByAuthor(Author author, SortCondition sortCondition = SortCondition.Newest, int maxCount = 0)
    {
        var postsByAuthor = CachedPosts.Where(post => post.AuthorId.Equals(author.Id));
        var byAuthor = postsByAuthor as Post[] ?? postsByAuthor.ToArray();
        return byAuthor.SortByCondition(sortCondition).Take(maxCount > 0 ? maxCount : byAuthor.Length);
    }

    public IEnumerable<Post> GetPosts(SortCondition sortCondition = SortCondition.Newest, int maxCount = 0) =>
        CachedPosts.SortByCondition(sortCondition).Take(maxCount > 0 ? maxCount : CachedPosts.Count());

    public IEnumerable<Post> GetPostsByTopic(Topic topic, SortCondition sortCondition = SortCondition.Newest, int maxCount = 0)
    {
        var postsByTopic = CachedPosts.Where(post => post.MainTopic.ToLower().Equals(topic.MainTopic.ToLower()));
        var byTopic = postsByTopic as Post[] ?? postsByTopic.ToArray();
        return byTopic.SortByCondition(sortCondition).Take(maxCount > 0 ? maxCount : byTopic.Length);
    }
}