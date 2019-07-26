using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    /// <remarks>
    /// Spec: https://github.com/simplepie/simplepie-ng/wiki/Spec:-Well-Formed-Web
    /// </remarks>
    public static class WfwExtensionConstants
    {
        public const string NamespaceAlias = "wfw";
        public static readonly XNamespace Namespace = "http://wellformedweb.org/CommentAPI/";
    }
}