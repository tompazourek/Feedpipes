using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/slash/
    /// </remarks>
    internal static class Rss10SlashConstants
    {
        public const string NamespaceAlias = "slash";
        public static readonly XNamespace Namespace = "http://purl.org/rss/1.0/modules/slash/";
    }
}