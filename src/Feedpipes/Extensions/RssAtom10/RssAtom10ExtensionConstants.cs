using System.Xml.Linq;
using Feedpipes.Atom10;

namespace Feedpipes.Extensions.RssAtom10
{
    /// <remarks>
    /// Atom 1.0 used as extension for RSS.
    /// Spec: https://validator.w3.org/feed/docs/atom.html
    /// </remarks>
    internal static class RssAtom10ExtensionConstants
    {
        public static readonly XNamespace Namespace = Atom10Constants.Namespace;
        public static readonly XNamespace[] RecognizedNamespaces = Atom10Constants.RecognizedNamespaces;
        public static string NamespaceAlias => "atom";
    }
}
