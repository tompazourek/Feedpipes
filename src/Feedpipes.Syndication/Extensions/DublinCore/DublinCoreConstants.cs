using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.DublinCore
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/dc/
    /// </remarks>
    internal static class DublinCoreConstants
    {
        public const string NamespaceAlias = "dc";
        public static readonly XNamespace Namespace = "http://purl.org/dc/elements/1.1/";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://purl.org/dc/elements/1.1/",
            "https://purl.org/dc/elements/1.1/",
        };
    }
}