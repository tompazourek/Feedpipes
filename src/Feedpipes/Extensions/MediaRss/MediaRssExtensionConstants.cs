using System.Xml.Linq;

namespace Feedpipes.Extensions.MediaRss
{
    /// <remarks>
    /// Spec: http://www.rssboard.org/media-rss
    /// </remarks>
    internal static class MediaRssExtensionConstants
    {
        public const string NamespaceAlias = "media";
        public static readonly XNamespace Namespace = "http://search.yahoo.com/mrss/";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://search.yahoo.com/mrss/",
            "https://search.yahoo.com/mrss/",
            "http://search.yahoo.com/mrss",
            "https://search.yahoo.com/mrss",
            "http://www.rssboard.org/media-rss",
            "https://www.rssboard.org/media-rss",
        };
    }
}
