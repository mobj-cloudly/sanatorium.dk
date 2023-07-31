using System.Globalization;

namespace Sanatorium.Core.Posts;

public class Post
{
    public string Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string PublishDate => Timestamp.ToString("D", new CultureInfo("da-DK"));
    public string Title { get; set; }
    public string TitleEncoded { get; set; }
    public string MainTopic { get; set; }
    public string MainTopicEncoded{ get; set; }
    public string PostLink => $"{MainTopicEncoded}/{TitleEncoded}";
    public string SubTopic { get; set; }
    public string AuthorId { get; set; }
    public int ReadTimeMinutes { get; set; }
    public long ViewCount { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<PostElement> Body { get; set; }

}

public class PostElement
{
    public string ElementType { get; set; } = PostElementType.Paragraph.ToString();
    public string[] Bullets { get; set; }
    public string PreText { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ImageText { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;
    public string LinkText { get; set; } = string.Empty;
    public string PostText { get; set; } = string.Empty;

}
public enum PostElementType { Headline, SubHeadline, BodyImage, BigQuote, Quote, Paragraph, ParagraphWithLink, BulletList}