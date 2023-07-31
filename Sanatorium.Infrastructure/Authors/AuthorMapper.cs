using System.Text.RegularExpressions;
using Sanatorium.Core.Authors;

namespace Sanatorium.Infrastructure.Authors;

public static partial class AuthorMapper
{
    public const string PartitionKey = "Authors";

    public static Author ToAuthor(this AuthorEntity authorEntity) =>
        new ()
        {
            Id = authorEntity.Id,
            Name = authorEntity.Name,
            NameEncoded = authorEntity.NameEncoded,
            Title = authorEntity.Title,
            Description = authorEntity.Description,
            ImageUrl = authorEntity.ImageUrl,
            FacebookUrl = authorEntity.FacebookUrl,
            InstagramUrl = authorEntity.InstagramUrl,
            TwitterUrl = authorEntity.TwitterUrl,
            LinkedinUrl = authorEntity.LinkedinUrl
        };

    public static AuthorEntity ToAuthorEntity(this Author author) =>
        new()
        {
            PartitionKey = PartitionKey,
            RowKey = author.Id,
            Id = author.Id,
            Name = author.Name,
            NameEncoded = Encode(author.Name),
            Title = author.Title,
            Description = author.Description,
            ImageUrl = author.ImageUrl,
            FacebookUrl = author.FacebookUrl,
            InstagramUrl = author.InstagramUrl,
            TwitterUrl = author.TwitterUrl,
            LinkedinUrl = author.LinkedinUrl
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

    [GeneratedRegex("[^a-æøåA-ÆØÅ0-9 -]")]
    private static partial Regex MyRegex();
}