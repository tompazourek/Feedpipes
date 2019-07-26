namespace Feedpipes.Syndication.Extensions.Rss10Content.Entities
{
    /// <summary>
    /// Corresponds to "content:encoded" element.
    /// An element whose contents are the entity-encoded or CDATA-escaped version of the content of the item. 
    /// </summary>
    public class Rss10ContentEncoded : IFeedExtensionEntity
    {
        public string Content { get; set; }
    }
}
