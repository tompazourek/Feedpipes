namespace Feedpipes.Syndication.Rss20.Document
{
    /// <summary>
    /// Root RSS 2.0 element. Corresponds to "rss" element.
    /// </summary>
    /// <remarks>
    /// Based on: https://validator.w3.org/feed/docs/rss2.html
    /// </remarks>
    public class Rss20Feed
    {
        public Rss20Channel Channel { get; set; }
    }
}