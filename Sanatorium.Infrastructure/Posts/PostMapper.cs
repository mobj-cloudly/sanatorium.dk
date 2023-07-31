using System.Text.Json;
using System.Text.RegularExpressions;
using Sanatorium.Core.Posts;

namespace Sanatorium.Infrastructure.Posts;

public static partial class PostMapper
{
    public const string PartitionKey = "Posts";

    public static Post ToPost(this PostEntity postEntity) =>
        new ()
        {
            Id = postEntity.Id,
            Timestamp = postEntity.Timestamp??DateTimeOffset.Now,
            Title = postEntity.Title,
            TitleEncoded = postEntity.TitleEncoded,
            MainTopic = postEntity.MainTopic,
            MainTopicEncoded = postEntity.MainTopicEncoded,
            SubTopic = postEntity.Subtopic,
            AuthorId = postEntity.AuthorId,
            ReadTimeMinutes = postEntity.ReadTimeMinutes,
            Tags = DeserializeTags(postEntity.TagsJson),
            ImageUrl = postEntity.ImageUrl,
            Body = DeserializePostBody(postEntity.BodyJson)
        };

    private static IEnumerable<PostElement> DeserializePostBody(string postEntityBodyJson) =>
        JsonSerializer.Deserialize<IEnumerable<PostElement>>(postEntityBodyJson) ?? throw new PostDeserializeException();

    private static IEnumerable<string> DeserializeTags(string tagsJson) =>
        JsonSerializer.Deserialize<IEnumerable<string>>(tagsJson) ?? throw new PostDeserializeException();

    public static PostEntity ToPostEntity(this Post post) =>
        new()
        {
            PartitionKey = PartitionKey,
            RowKey = post.Id,
            Timestamp = DateTimeOffset.Now,
            Id = post.Id,
            Title = post.Title,
            TitleEncoded = Encode(post.Title),
            MainTopic = post.MainTopic,
            MainTopicEncoded = Encode(post.MainTopic),
            Subtopic = post.SubTopic,
            AuthorId = post.AuthorId,
            ReadTimeMinutes = post.ReadTimeMinutes,
            TagsJson = SerializeTags(post.Tags),
            ImageUrl = post.ImageUrl,
            BodyJson = SerializePostBody(post.Body)
        };

    private static readonly Regex Regex = MyRegex();

    private static string Encode(string postTitle) => Regex
        .Replace(postTitle, string.Empty)
        .Replace(" ", "-")
        .Replace("æ", "ae")
        .Replace("Æ", "Ae")
        .Replace("ø", "oe")
        .Replace("Ø", "Oe")
        .Replace("å", "aa")
        .Replace("Å", "Aa");

    private static string SerializePostBody(IEnumerable<PostElement> postBody) =>
        JsonSerializer.Serialize(postBody);

    private static string SerializeTags(IEnumerable<string> postTags) =>
        JsonSerializer.Serialize(postTags);

    [GeneratedRegex("[^a-æøåA-ÆØÅ0-9 -]")]
    private static partial Regex MyRegex();
}