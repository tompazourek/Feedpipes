using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Extensions.Rss10Syndication.Entities;
using Feedpipes.Timestamps.Relaxed;

namespace Feedpipes.Extensions.Rss10Syndication
{
    internal static class Rss10SyndicationExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10SyndicationExtension(XElement channelElement, out Rss10SyndicationExtension extension)
        {
            extension = null;

            if (channelElement == null)
                return false;

            foreach (var ns in Rss10SyndicationExtensionConstants.RecognizedNamespaces)
            {
                if (TryParseRss10SyndicationUpdatePeriod(channelElement.Element(ns + "updatePeriod"), out var parsedUpdatePeriod))
                {
                    extension = extension ?? new Rss10SyndicationExtension();
                    extension.UpdatePeriod = parsedUpdatePeriod;
                }

                if (TryParseRss10SyndicationUpdateFrequency(channelElement.Element(ns + "updateFrequency"), out var parsedUpdateFrequency))
                {
                    extension = extension ?? new Rss10SyndicationExtension();
                    extension.UpdateFrequency = parsedUpdateFrequency;
                }

                if (TryParseRss10SyndicationUpdateBase(channelElement.Element(ns + "updateBase"), out var parsedUpdateBase))
                {
                    extension = extension ?? new Rss10SyndicationExtension();
                    extension.UpdateBase = parsedUpdateBase;
                }
            }

            return extension != null;
        }

        private static bool TryParseRss10SyndicationUpdatePeriod(XElement element, out Rss10SyndicationUpdatePeriodValue parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim().ToLowerInvariant();
            switch (valueString)
            {
                case "hourly":
                    parsedValue = Rss10SyndicationUpdatePeriodValue.Hourly;
                    break;
                case "daily":
                    parsedValue = Rss10SyndicationUpdatePeriodValue.Daily;
                    break;
                case "weekly":
                    parsedValue = Rss10SyndicationUpdatePeriodValue.Weekly;
                    break;
                case "monthly":
                    parsedValue = Rss10SyndicationUpdatePeriodValue.Monthly;
                    break;
                case "yearly":
                    parsedValue = Rss10SyndicationUpdatePeriodValue.Yearly;
                    break;
                default:
                    return false;
            }

            return true;
        }

        private static bool TryParseRss10SyndicationUpdateFrequency(XElement element, out int parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            return int.TryParse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue);
        }

        private static bool TryParseRss10SyndicationUpdateBase(XElement element, out DateTimeOffset parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            if (!RelaxedTimestampParser.TryParseTimestampFromString(element.Value, out parsedValue))
                return false;

            return true;
        }
    }
}