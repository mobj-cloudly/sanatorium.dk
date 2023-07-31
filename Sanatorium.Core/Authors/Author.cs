namespace Sanatorium.Core.Authors;

public class Author
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string NameEncoded { get; set; }
    public string AuthorLink => $"Skribent/{NameEncoded}";
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string LinkedinUrl { get; set; }
}