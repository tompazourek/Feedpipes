using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;
using Feedpipes.Syndication.Timestamps.Rfc3339;
using Feedpipes.Syndication.Xml;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    internal static class Rss10SyndicationChannelExtensionFormatter
    {
        public static bool TryFormatRss10SyndicationChannelExtension(Rss10SyndicationChannelExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatRss10SyndicationUpdatePeriod(extensionToFormat.UpdatePeriod, namespaceAliases, out var updatePeriodElement))
            {
                elements.Add(updatePeriodElement);
            }

            if (TryFormatRss10SyndicationUpdateFrequency(extensionToFormat.UpdateFrequency, namespaceAliases, out var updateFrequencyElement))
            {
                elements.Add(updateFrequencyElement);
            }

            if (TryFormatRss10SyndicationUpdateBase(extensionToFormat.UpdateBase, namespaceAliases, out var updateBaseElement))
            {
                elements.Add(updateBaseElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatRss10SyndicationUpdatePeriod(Rss10SyndicationUpdatePeriodValue? valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            element = new XElement(Rss10SyndicationConstants.Namespace + "updatePeriod");

            string updatePeriodString;
            switch (valueToFormat.Value)
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

            namespaceAliases.EnsureNamespaceAlias(Rss10SyndicationConstants.NamespaceAlias, Rss10SyndicationConstants.Namespace);
            return true;
        }

        private static bool TryFormatRss10SyndicationUpdateFrequency(int? valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            var valueString = valueToFormat.Value.ToString(CultureInfo.InvariantCulture);
            namespaceAliases.EnsureNamespaceAlias(Rss10SyndicationConstants.NamespaceAlias, Rss10SyndicationConstants.Namespace);
            element = new XElement(Rss10SyndicationConstants.Namespace + "updateFrequency") { Value = valueString };

            return true;
        }

        private static bool TryFormatRss10SyndicationUpdateBase(DateTimeOffset? valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(valueToFormat.Value, out var valueString))
                return false;

            namespaceAliases.EnsureNamespaceAlias(Rss10SyndicationConstants.NamespaceAlias, Rss10SyndicationConstants.Namespace);
            element = new XElement(Rss10SyndicationConstants.Namespace + "updateBase") { Value = valueString };

            return true;
        }
    }
}