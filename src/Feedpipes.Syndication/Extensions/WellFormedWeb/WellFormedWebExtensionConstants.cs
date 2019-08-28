using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    /// <remarks>
    /// Spec: https://github.com/simplepie/simplepie-ng/wiki/Spec:-Well-Formed-Web
    /// </remarks>
    internal static class WellFormedWebExtensionConstants
    {
        public const string NamespaceAlias = "wfw";
        public static readonly XNamespace Namespace = "http://wellformedweb.org/CommentAPI/";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://wellformedweb.org/CommentAPI/",
            "https://wellformedweb.org/CommentAPI/",
        };
    }
}