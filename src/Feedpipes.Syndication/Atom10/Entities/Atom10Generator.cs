namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "generator" element.
    /// Identifies the software used to generate the feed, for debugging and other purposes.
    /// </summary>
    public class Atom10Generator
    {
        /// <summary>
        /// Optional "uri" attribute.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Optional "version" attribute.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Contents of the "generator" element.
        /// </summary>
        public string Value { get; set; }
    }
}