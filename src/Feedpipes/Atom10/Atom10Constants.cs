using System.Xml.Linq;

namespace Feedpipes.Atom10
{
    /// <remarks>
    /// Spec: https://validator.w3.org/feed/docs/atom.html
    /// </remarks>
    internal static class Atom10Constants
    {
        public static readonly XNamespace Namespace = "http://www.w3.org/2005/Atom";
        public static readonly XNamespace XhtmlNamespace = "http://www.w3.org/1999/xhtml";

        public static readonly XNamespace[] RecognizedXhtmlNamespaces =
        {
            "http://www.w3.org/1999/xhtml",
            "https://www.w3.org/1999/xhtml",
        };

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://www.w3.org/2005/Atom",
            "https://www.w3.org/2005/Atom",
        };
    }
}
