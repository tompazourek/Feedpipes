using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    internal static class Rss10ContentItemExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10ContentItemExtension(XElement itemElement, out Rss10ContentItemExtension extension)
        {
            extension = null;

            if (itemElement == null)
                return false;

            if (TryParseRss10ContentEncoded(itemElement.Element(Rss10ContentConstants.Namespace + "encoded"), out var parsedEncoded))
            {
                extension = extension ?? new Rss10ContentItemExtension();
                extension.Encoded = parsedEncoded;
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