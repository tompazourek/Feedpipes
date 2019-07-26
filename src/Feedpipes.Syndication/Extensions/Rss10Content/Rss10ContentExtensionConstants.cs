using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    /// <remarks>
    /// Spec: http://web.resource.org/rss/1.0/modules/content/
    /// </remarks>
    public static class Rss10ContentExtensionConstants
    {
        public const string NamespaceAlias = "content";
        public static readonly XNamespace Namespace = "http://purl.org/rss/1.0/modules/content/";
    }
}