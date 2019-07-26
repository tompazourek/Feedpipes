namespace Feedpipes.Syndication.Extensions.Rss10Slash.Entities
{
    /// <summary>
    /// Corresponds to "slash:comments" element.
    /// </summary>
    public class Rss10SlashComments : IFeedExtensionEntity
    {
        public int Count { get; set; }
    }
}