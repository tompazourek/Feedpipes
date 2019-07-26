namespace Feedpipes.Syndication.Rss20Feed.Document
{
    /// <summary>
    /// Describes a media object that is attached to the item.
    /// </summary>
    public class Rss20Enclosure
    {
        /// <summary>
        /// URL says where the enclosure is located.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Length says how big it is in bytes.
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// Type says what its type is, a standard MIME type.
        /// </summary>
        public string Type { get; set; }
    }
}