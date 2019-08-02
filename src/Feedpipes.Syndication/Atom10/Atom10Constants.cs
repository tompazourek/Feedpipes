using System.Xml.Linq;

namespace Feedpipes.Syndication.Atom10
{
    /// <remarks>
    /// Spec: https://validator.w3.org/feed/docs/atom.html
    /// </remarks>
    internal static class Atom10Constants
    {
        public static readonly XNamespace Namespace = "http://www.w3.org/2005/Atom";

        public static readonly XNamespace[] RecognizedNamespaces =
        {
            "http://www.w3.org/2005/Atom",
            "https://www.w3.org/2005/Atom",
            "http://purl.org/atom/ns#", // Atom 0.3 namespace
            "https://purl.org/atom/ns#",
        };
    }
}