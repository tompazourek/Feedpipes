using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    internal static class Rss10SlashItemExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10SlashItemExtension(XElement itemElement, out Rss10SlashItemExtension extension)
        {
            extension = null;

            if (itemElement == null)
                return false;

            foreach (var ns in Rss10SlashConstants.RecognizedNamespaces)
            {
                if (TryParseRss10SlashTextElement(itemElement.Element(ns + "section"), out var parsedSection))
                {
                    extension = extension ?? new Rss10SlashItemExtension();
                    extension.Section = parsedSection;
                }

                if (TryParseRss10SlashTextElement(itemElement.Element(ns + "department"), out var parsedDepartment))
                {
                    extension = extension ?? new Rss10SlashItemExtension();
                    extension.Department = parsedDepartment;
                }

                if (TryParseRss10SlashComments(itemElement.Element(ns + "comments"), out var parsedComments))
                {
                    extension = extension ?? new Rss10SlashItemExtension();
                    extension.Comments = parsedComments;
                }

                if (TryParseRss10SlashHitParade(itemElement.Element(ns + "hit_parade"), out var parsedHitParade))
                {
                    extension = extension ?? new Rss10SlashItemExtension();
                    extension.HitParade = parsedHitParade;
                }
            }

            return extension != null;
        }

        private static bool TryParseRss10SlashTextElement(XElement element, out string parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            parsedValue = element.Value;
            return true;
        }

        private static bool TryParseRss10SlashComments(XElement element, out int parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            return int.TryParse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue);
        }

        private static bool TryParseRss10SlashHitParade(XElement element, out IList<int> parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            var valueStringParts = valueString.Split(",").Select(x => x.Trim());

            parsedValue = new List<int>();

            foreach (var valueStringPart in valueStringParts)
            {
                if (!int.TryParse(valueStringPart, NumberStyles.Any, CultureInfo.InvariantCulture, out var valueInt))
                    continue;

                parsedValue.Add(valueInt);
            }

            if (!parsedValue.Any())
                return false;

            return true;
        }
    }
}