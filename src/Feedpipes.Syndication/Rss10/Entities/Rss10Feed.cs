namespace Feedpipes.Syndication.Rss10.Entities
{
    /// <summary>
    /// Root RSS 1.0 RDF element. Corresponds to "rss" element.
    /// </summary>
    /// <remarks>
    /// Based on: http://web.resource.org/rss/1.0/spec
    /// </remarks>
    public class Rss10Feed
    {
        public Rss10Channel Channel { get; set; }
    }
}