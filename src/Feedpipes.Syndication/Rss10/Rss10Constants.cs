using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/spec
    /// </remarks>
    internal static class Rss10Constants
    {
        public const string RdfNamespaceAlias = "rdf";
        public static readonly XNamespace RdfNamespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        public static readonly XNamespace Rss10Namespace = "http://purl.org/rss/1.0/";
    }
}