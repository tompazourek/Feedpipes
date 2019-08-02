using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;
using Feedpipes.Syndication.RelaxedTimestamp;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    internal static class Rss10SyndicationChannelExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRss10SyndicationChannelExtension(XElement channelElement, out Rss10SyndicationChannelExtension extension)
        {
            extension = null;

            if (channelElement == null)
                return false;

            if (TryParseRss10SyndicationUpdatePeriod(channelElement.Element(Rss10SyndicationConstants.Namespace + "updatePeriod"), out var parsedUpdatePeriod))
            {
                extension = extension ?? new Rss10SyndicationChannelExtension();
                extension.UpdatePeriod = parsedUpdatePeriod;
            }

            if (TryParseRss10SyndicationUpdateFrequency(channelElement.Element(Rss10SyndicationConstants.Namespace + "updateFrequency"), out var parsedUpdateFrequency))
            {
                extension = extension ?? new Rss10SyndicationChannelExtension();
                extension.UpdateFrequency = parsedUpdateFrequency;
            }

            if (TryParseRss10SyndicationUpdateBase(channelElement.Element(Rss10SyndicationConstants.Namespace + "updateBase"), out var parsedUpdateBase))
            {
                extension = extension ?? new Rss10SyndicationChannelExtension();
                extension.UpdateBase = parsedUpdateBase;
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