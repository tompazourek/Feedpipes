namespace Feedpipes.Syndication.Extensions.Rss10Content.Entities
{
    public class Rss10ContentItemExtension
    {
        /// <summary>
        /// Corresponds to "content:encoded" element.
        /// An element whose contents are the entity-encoded or CDATA-escaped version of the content of the item.
        /// </summary>
        public string Encoded { get; set; }
    }
}