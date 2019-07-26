using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    public class Rss10SyndicationExtensionParser : IFeedExtensionParser
    {
        public IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement)
        {
            if (parentElement == null)
                yield break;

            // parse "sy:updatePeriod"
            foreach (var updatePeriodElement in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updatePeriod"))
            {
                if (!TryParseRss10SyndicationUpdatePeriod(updatePeriodElement, out var parsedUpdatePeriod))
                    continue;

                yield return parsedUpdatePeriod;
            }

            // parse "sy:updateFrequency"
            foreach (var updateFrequencyElement in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updateFrequency"))
            {
                if (!TryParseRss10SyndicationUpdateFrequency(updateFrequencyElement, out var parsedUpdateFrequency))
                    continue;

                yield return parsedUpdateFrequency;
            }

            // parse "sy:updateBase"
            foreach (var updateBaseElement in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updateBase"))
            {
                if (!TryParseRss10SyndicationUpdateBase(updateBaseElement, out var parsedUpdateBase))
                    continue;

                yield return parsedUpdateBase;
            }
        }

        private bool TryParseRss10SyndicationUpdatePeriod(XElement updatePeriodElement, out Rss10SyndicationUpdatePeriod parsedUpdatePeriod)
        {
            parsedUpdatePeriod = default;

            if (updatePeriodElement == null)
                return false;

            var valueString = updatePeriodElement.Value.Trim().ToLowerInvariant();
            Rss10SyndicationUpdatePeriodValue valueEnum;
            switch (valueString)
            {
                case "hourly":
                    valueEnum = Rss10SyndicationUpdatePeriodValue.Hourly;
                    break;
                case "daily":
                    valueEnum = Rss10SyndicationUpdatePeriodValue.Daily;
                    break;
                case "weekly":
                    valueEnum = Rss10SyndicationUpdatePeriodValue.Weekly;
                    break;
                case "monthly":
                    valueEnum = Rss10SyndicationUpdatePeriodValue.Monthly;
                    break;
                case "yearly":
                    valueEnum = Rss10SyndicationUpdatePeriodValue.Yearly;
                    break;
                default:
                    return false;
            }

            parsedUpdatePeriod = new Rss10SyndicationUpdatePeriod { Value = valueEnum };
            return true;
        }

        private bool TryParseRss10SyndicationUpdateFrequency(XElement updateFrequencyElement, out Rss10SyndicationUpdateFrequency parsedUpdateFrequency)
        {
            parsedUpdateFrequency = default;

            if (updateFrequencyElement == null)
                return false;

            var valueString = updateFrequencyElement.Value.Trim().ToLowerInvariant();
            if (!int.TryParse(valueString, out var valueInt))
                return false;

            parsedUpdateFrequency = new Rss10SyndicationUpdateFrequency { Frequency = valueInt };
            return true;
        }

        private bool TryParseRss10SyndicationUpdateBase(XElement updateBaseElement, out Rss10SyndicationUpdateBase parsedUpdateBase)
        {
            parsedUpdateBase = default;

            if (updateBaseElement == null)
                return false;

            var valueString = updateBaseElement.Value.Trim().ToUpperInvariant();

            if (!DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mmzzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out var valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp))
                return false;

            parsedUpdateBase = new Rss10SyndicationUpdateBase { Timestamp = valueTimestamp };
            return true;
        }
    }
}