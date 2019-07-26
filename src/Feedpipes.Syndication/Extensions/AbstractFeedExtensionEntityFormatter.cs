using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;

namespace Feedpipes.Syndication.Extensions
{
    public class AbstractFeedExtensionEntityFormatter
    {
        private readonly IEnumerable<IFeedExtensionFormatter> _feedExtensionFormatters;

        public AbstractFeedExtensionEntityFormatter()
        {
            _feedExtensionFormatters = new IFeedExtensionFormatter[]
            {
                new Rss10ContentExtensionFormatter(),
                new Rss10SyndicationExtensionFormatter(),
                new Rss10SlashExtensionFormatter(),
            };
        }

        public IEnumerable<XElement> FormatExtensionEntities(IEnumerable<IFeedExtensionEntity> extensionEntitiesToFormat, out IEnumerable<XAttribute> rootNamespaceAliases)
        {
            rootNamespaceAliases = Enumerable.Empty<XAttribute>();

            if (extensionEntitiesToFormat == null)
                return Enumerable.Empty<XElement>();

            var results = new List<XElement>();
            var rootNamespaceAliasesList = new List<XAttribute>();

            foreach (var extensionEntityToFormat in extensionEntitiesToFormat)
            {
                foreach (var extensionFormatter in _feedExtensionFormatters)
                {
                    if (!extensionFormatter.TryFormatExtensionEntity(extensionEntityToFormat, out var element))
                        continue;

                    results.Add(element);
                    rootNamespaceAliasesList.Add(new XAttribute(XNamespace.Xmlns + extensionFormatter.GetNamespaceAlias(), extensionFormatter.GetNamespace().NamespaceName));
                }
            }

            rootNamespaceAliases = rootNamespaceAliasesList;
            return results;
        }
    }
}