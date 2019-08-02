using System.Xml.Linq;

namespace Feedpipes.Syndication.Rss10
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/spec
    /// </remarks>
    internal static class Rss10Constants
    {
        public const string RdfNamespaceAlias = "rdf";
        public static readonly XNamespace RdfNamespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        public static readonly XNamespace Namespace = "http://purl.org/rss/1.0/";
        
        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://purl.org/rss/1.0/",
            "https://purl.org/rss/1.0/",
        };
    }
}