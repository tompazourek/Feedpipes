using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.Rss10Slash.Entities;

namespace Feedpipes.Extensions.Rss10Slash
{
    internal static class Rss10SlashExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10SlashExtension(XElement parentElement, out Rss10SlashExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            foreach (var ns in Rss10SlashExtensionConstants.RecognizedNamespaces)
            {
                if (TryParseRss10SlashTextElement(parentElement.Element(ns + "section"), out var parsedSection))
                {
                    extension = extension ?? new Rss10SlashExtension();
                    extension.Section = parsedSection;
                }

                if (TryParseRss10SlashTextElement(parentElement.Element(ns + "department"), out var parsedDepartment))
                {
                    extension = extension ?? new Rss10SlashExtension();
                    extension.Department = parsedDepartment;
                }

                if (TryParseRss10SlashComments(parentElement.Element(ns + "comments"), out var parsedComments))
                {
                    extension = extension ?? new Rss10SlashExtension();
                    extension.Comments = parsedComments;
                }

                if (TryParseRss10SlashHitParade(parentElement.Element(ns + "hit_parade"), out var parsedHitParade))
                {
                    extension = extension ?? new Rss10SlashExtension();
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