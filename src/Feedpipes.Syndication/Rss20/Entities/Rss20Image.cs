namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// Specifies a GIF, JPEG or PNG image that can be displayed with the channel.
    /// </summary>
    public class Rss20Image
    {
        /// <summary>
        /// URL of a GIF, JPEG or PNG image that represents the channel.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Describes the image, it's used in the ALT attribute of the HTML &lt;img&gt; tag when the channel is rendered in HTML.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL of the site, when the channel is rendered, the image is a link to the site.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Width of the image in pixels.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Height of the image in pixels.
        /// </summary>
        public int? Height { get; set; }
    }
}