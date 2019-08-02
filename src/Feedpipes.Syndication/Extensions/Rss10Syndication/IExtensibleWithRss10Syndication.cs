using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    public interface IExtensibleWithRss10Syndication
    {
        /// <summary>
        /// Optional "sy:*" extended information.
        /// </summary>
        Rss10SyndicationChannelExtension SyndicationExtension { get; set; }
    }
}