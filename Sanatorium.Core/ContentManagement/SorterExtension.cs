using System.Diagnostics;
using Sanatorium.Core.Posts;

namespace Sanatorium.Core.ContentManagement;

public static class SorterExtension
{
    private static readonly Random Random = new Random();
    public static IEnumerable<Post> SortByCondition(this IEnumerable<Post> posts, SortCondition sortCondition) =>
        sortCondition switch
        {
            SortCondition.Random => posts.OrderBy(order=> Random.Next()),
            SortCondition.Newest => posts.OrderByDescending(post => post.Timestamp),
            SortCondition.MostViews => posts.OrderByDescending(post => post.ViewCount),
            _ => throw new ArgumentOutOfRangeException(nameof(sortCondition), sortCondition, null)
        };
}