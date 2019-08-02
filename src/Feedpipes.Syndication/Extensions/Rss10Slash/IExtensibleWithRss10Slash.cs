using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    public interface IExtensibleWithRss10Slash
    {
        /// <summary>
        /// Optional "slash:*" extended information.
        /// </summary>
        Rss10SlashItemExtension SlashExtension { get; set; }
    }
}