using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.CreativeCommons
{
    /// <remarks>
    /// Spec: http://backend.userland.com/creativeCommonsRssModule
    /// </remarks>
    internal static class CreativeCommonsExtensionConstants
    {
        public const string NamespaceAlias = "cc";
        public static readonly XNamespace Namespace = "http://backend.userland.com/creativeCommonsRssModule";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://backend.userland.com/creativeCommonsRssModule",
            "https://backend.userland.com/creativeCommonsRssModule",
            "http://cyber.law.harvard.edu/rss/creativeCommonsRssModule.html",
            "https://cyber.law.harvard.edu/rss/creativeCommonsRssModule.html",
        };
    }
}