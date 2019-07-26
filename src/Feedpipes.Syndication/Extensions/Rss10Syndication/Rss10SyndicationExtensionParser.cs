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

            foreach (var element in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updatePeriod"))
            {
                if (!TryParseRss10SyndicationUpdatePeriod(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updateFrequency"))
            {
                if (!TryParseRss10SyndicationUpdateFrequency(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(Rss10SyndicationExtensionConstants.Namespace + "updateBase"))
            {
                if (!TryParseRss10SyndicationUpdateBase(element, out var entity))
                    continue;

                yield return entity;
            }
        }

        private bool TryParseRss10SyndicationUpdatePeriod(XElement element, out Rss10SyndicationUpdatePeriod entity)
        {
            entity = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim().ToLowerInvariant();
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

            entity = new Rss10SyndicationUpdatePeriod { Value = valueEnum };
            return true;
        }

        private bool TryParseRss10SyndicationUpdateFrequency(XElement element, out Rss10SyndicationUpdateFrequency entity)
        {
            entity = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim();
            if (!int.TryParse(valueString, out var valueInt))
                return false;

            entity = new Rss10SyndicationUpdateFrequency { Frequency = valueInt };
            return true;
        }

        private bool TryParseRss10SyndicationUpdateBase(XElement element, out Rss10SyndicationUpdateBase entity)
        {
            entity = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim().ToUpperInvariant();

            if (!DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mmzzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out var valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp) &&
                !DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out valueTimestamp))
                return false;

            entity = new Rss10SyndicationUpdateBase { Timestamp = valueTimestamp };
            return true;
        }
    }
}