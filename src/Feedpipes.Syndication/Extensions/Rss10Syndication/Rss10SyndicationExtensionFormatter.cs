using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    public class Rss10SyndicationExtensionFormatter : IFeedExtensionFormatter
    {
        public string GetNamespaceAlias() => Rss10SyndicationExtensionConstants.NamespaceAlias;
        public XNamespace GetNamespace() => Rss10SyndicationExtensionConstants.Namespace;

        public bool TryFormatExtensionEntity(IFeedExtensionEntity extensionEntityToFormat, out XElement element)
        {
            element = default;

            if (extensionEntityToFormat == null)
                return false;

            switch (extensionEntityToFormat)
            {
                // format "sy:updatePeriod"
                case Rss10SyndicationUpdatePeriod updatePeriodToFormat:
                    if (TryFormatRss10SyndicationUpdatePeriod(updatePeriodToFormat, out element))
                        return true;
                    break;

                // format "sy:updateFrequency"
                case Rss10SyndicationUpdateFrequency updateFrequencyToFormat:
                    if (TryFormatRss10SyndicationUpdateFrequency(updateFrequencyToFormat, out element))
                        return true;
                    break;

                // format "sy:updateBase"
                case Rss10SyndicationUpdateBase updateBaseToFormat:
                    if (TryFormatRss10SyndicationUpdateBase(updateBaseToFormat, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatRss10SyndicationUpdatePeriod(Rss10SyndicationUpdatePeriod updatePeriodToFormat, out XElement encodedElement)
        {
            encodedElement = default;

            if (updatePeriodToFormat == null)
                return false;

            encodedElement = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updatePeriod");

            string updatePeriodString;
            switch (updatePeriodToFormat.Value)
            {
                case Rss10SyndicationUpdatePeriodValue.Hourly:
                    updatePeriodString = "hourly";
                    break;
                case Rss10SyndicationUpdatePeriodValue.Daily:
                    updatePeriodString = "daily";
                    break;
                case Rss10SyndicationUpdatePeriodValue.Weekly:
                    updatePeriodString = "weekly";
                    break;
                case Rss10SyndicationUpdatePeriodValue.Monthly:
                    updatePeriodString = "monthly";
                    break;
                case Rss10SyndicationUpdatePeriodValue.Yearly:
                    updatePeriodString = "yearly";
                    break;
                default:
                    return false;
            }

            encodedElement.Value = updatePeriodString;

            return true;
        }

        private bool TryFormatRss10SyndicationUpdateFrequency(Rss10SyndicationUpdateFrequency updateFrequencyToFormat, out XElement encodedElement)
        {
            encodedElement = default;

            if (updateFrequencyToFormat == null)
                return false;

            var valueString = updateFrequencyToFormat.Frequency.ToString(CultureInfo.InvariantCulture);
            encodedElement = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updateFrequency") { Value = valueString };

            return true;
        }

        private bool TryFormatRss10SyndicationUpdateBase(Rss10SyndicationUpdateBase updateBaseToFormat, out XElement encodedElement)
        {
            encodedElement = default;

            if (updateBaseToFormat == null)
                return false;

            var valueString = updateBaseToFormat.Timestamp.ToString("yyyy'-'MM'-'dd'T'HH':'mmzzz", CultureInfo.InvariantCulture);
            encodedElement = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updateBase") { Value = valueString };

            return true;
        }
    }
}