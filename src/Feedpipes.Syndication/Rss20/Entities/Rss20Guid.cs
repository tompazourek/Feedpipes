namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// "guid" element of items.
    /// guid stands for globally unique identifier. It's a string that uniquely identifies the item. When present,
    /// an aggregator may choose to use this string to determine if an item is new.
    /// There are no rules for the syntax of a guid. Aggregators must view them as a string.
    /// It's up to the source of the feed to establish the uniqueness of the string.
    /// </summary>
    public class Rss20Guid
    {
        /// <summary>
        /// Value of the guid.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If the guid element has an attribute named "isPermaLink" with a value of true,
        /// the reader may assume that it is a permalink to the item, that is, a url that can be
        /// opened in a Web browser, that points to the full item described by the "item" element.
        /// </summary>
        public bool? IsPermaLink { get; set; }
    }
}