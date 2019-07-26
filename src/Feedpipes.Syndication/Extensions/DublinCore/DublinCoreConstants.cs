using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/dc/
    /// </remarks>
    internal static class DublinCoreConstants
    {
        public const string NamespaceAlias = "dc";
        public static readonly XNamespace Namespace = "http://purl.org/dc/elements/1.1/";
    }
}