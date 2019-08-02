using Feedpipes.Syndication.Extensions.Rss10Content.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    public interface IExtensibleWithRss10Content
    {
        /// <summary>
        /// Optional "content:*" extended information.
        /// </summary>
        Rss10ContentItemExtension ContentExtension { get; set; }
    }
}