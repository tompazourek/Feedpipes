using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    internal static class Rss10ContentExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10ContentExtension(XElement itemElement, out Rss10ContentExtension extension)
        {
            extension = null;

            if (itemElement == null)
                return false;

            foreach (var ns in Rss10ContentExtensionConstants.RecognizedNamespaces)
            {
                if (TryParseRss10ContentEncoded(itemElement.Element(ns + "encoded"), out var parsedEncoded))
                {
                    extension = extension ?? new Rss10ContentExtension();
                    extension.Encoded = parsedEncoded;
                }
            }

            return extension != null;
        }

        private static bool TryParseRss10ContentEncoded(XElement element, out string parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            parsedValue = element.Value;
            return true;
        }
    }
}