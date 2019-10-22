using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    internal static class WellFormedWebExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseWellFormedWebExtension(XElement itemElement, out WellFormedWebExtension extension)
        {
            extension = null;

            if (itemElement == null)
                return false;

            foreach (var ns in WellFormedWebExtensionConstants.RecognizedNamespaces)
            {
                if (TryParseWfwTextElement(itemElement.Element(ns + "comment"), out var parsedComment))
                {
                    extension = extension ?? new WellFormedWebExtension();
                    extension.Comment = parsedComment;
                }

                if (TryParseWfwTextElement(itemElement.Element(ns + "commentRss"), out var parsedCommentRss))
                {
                    extension = extension ?? new WellFormedWebExtension();
                    extension.CommentRss = parsedCommentRss;
                }
                else if (TryParseWfwTextElement(itemElement.Element(ns + "commentRSS"), out var parsedCommentRSS))
                {
                    extension = extension ?? new WellFormedWebExtension();
                    extension.CommentRss = parsedCommentRSS;
                }
            }

            return extension != null;
        }

        private static bool TryParseWfwTextElement(XElement element, out string parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            parsedValue = element.Value;
            return true;
        }
    }
}