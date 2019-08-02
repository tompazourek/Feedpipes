using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    internal static class WfwItemExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseWfwItemExtension(XElement itemElement, out WfwItemExtension extension)
        {
            extension = null;

            if (itemElement == null)
                return false;

            foreach (var ns in WfwConstants.RecognizedNamespaces)
            {
                if (TryParseWfwTextElement(itemElement.Element(ns + "comment"), out var parsedComment))
                {
                    extension = extension ?? new WfwItemExtension();
                    extension.Comment = parsedComment;
                }

                if (TryParseWfwTextElement(itemElement.Element(ns + "commentRss"), out var parsedCommentRss))
                {
                    extension = extension ?? new WfwItemExtension();
                    extension.CommentRss = parsedCommentRss;
                }
                else if (TryParseWfwTextElement(itemElement.Element(ns + "commentRSS"), out var parsedCommentRSS))
                {
                    extension = extension ?? new WfwItemExtension();
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