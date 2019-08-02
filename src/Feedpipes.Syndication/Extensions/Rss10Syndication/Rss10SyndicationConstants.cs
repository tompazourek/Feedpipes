using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/syndication/
    /// </remarks>
    internal static class Rss10SyndicationConstants
    {
        public const string NamespaceAlias = "sy";
        public static readonly XNamespace Namespace = "http://purl.org/rss/1.0/modules/syndication/";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://purl.org/rss/1.0/modules/syndication/",
            "https://purl.org/rss/1.0/modules/syndication/",
        };
    }
}