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
                case Rss10SyndicationUpdatePeriod entity:
                    if (TryFormatRss10SyndicationUpdatePeriod(entity, out element))
                        return true;
                    break;

                case Rss10SyndicationUpdateFrequency entity:
                    if (TryFormatRss10SyndicationUpdateFrequency(entity, out element))
                        return true;
                    break;

                case Rss10SyndicationUpdateBase entity:
                    if (TryFormatRss10SyndicationUpdateBase(entity, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatRss10SyndicationUpdatePeriod(Rss10SyndicationUpdatePeriod entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updatePeriod");

            string updatePeriodString;
            switch (entity.Value)
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

            element.Value = updatePeriodString;

            return true;
        }

        private bool TryFormatRss10SyndicationUpdateFrequency(Rss10SyndicationUpdateFrequency entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            var valueString = entity.Frequency.ToString(CultureInfo.InvariantCulture);
            element = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updateFrequency") { Value = valueString };

            return true;
        }

        private bool TryFormatRss10SyndicationUpdateBase(Rss10SyndicationUpdateBase entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            var valueString = entity.Timestamp.ToString("yyyy'-'MM'-'dd'T'HH':'mmzzz", CultureInfo.InvariantCulture);
            element = new XElement(Rss10SyndicationExtensionConstants.Namespace + "updateBase") { Value = valueString };

            return true;
        }
    }
}