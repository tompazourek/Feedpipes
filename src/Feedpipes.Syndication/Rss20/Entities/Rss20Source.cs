namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// The RSS channel that the item came from.
    /// The purpose of this element is to propogate credit for links, to publicize the sources of news items.
    /// It's used in the post command in the Radio UserLand aggregator.
    /// It should be generated automatically when forwarding an item from an aggregator to a weblog authoring tool.
    /// </summary>
    public class Rss20Source
    {
        /// <summary>
        /// Its value is the name of the RSS channel that the item came from, derived from its "title".
        /// </summary>
        /// <example>
        /// Tomalak's Realm
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// URL, which links to the XMLization of the source.
        /// </summary>
        /// <example>
        /// http://static.userland.com/tomalak/links2.xml
        /// </example>
        public string Url { get; set; }
    }
}