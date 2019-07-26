using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Slash
{
    public class Rss10SlashExtensionFormatter : IFeedExtensionFormatter
    {
        public string GetNamespaceAlias() => Rss10SlashExtensionConstants.NamespaceAlias;
        public XNamespace GetNamespace() => Rss10SlashExtensionConstants.Namespace;

        public bool TryFormatExtensionEntity(IFeedExtensionEntity extensionEntityToFormat, out XElement element)
        {
            element = default;

            if (extensionEntityToFormat == null)
                return false;

            switch (extensionEntityToFormat)
            {
                case Rss10SlashSection entity:
                    if (TryFormatRss10SlashSection(entity, out element))
                        return true;
                    break;
                case Rss10SlashDepartment entity:
                    if (TryFormatRss10SlashDepartment(entity, out element))
                        return true;
                    break;
                case Rss10SlashComments entity:
                    if (TryFormatRss10SlashComments(entity, out element))
                        return true;
                    break;
                case Rss10SlashHitParade entity:
                    if (TryFormatRss10SlashHitParade(entity, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatRss10SlashSection(Rss10SlashSection entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(Rss10SlashExtensionConstants.Namespace + "section") { Value = entity.Value };
            return true;
        }

        private bool TryFormatRss10SlashDepartment(Rss10SlashDepartment entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(Rss10SlashExtensionConstants.Namespace + "department") { Value = entity.Value };
            return true;
        }

        private bool TryFormatRss10SlashComments(Rss10SlashComments entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            var valueString = entity.Count.ToString(CultureInfo.InvariantCulture);
            element = new XElement(Rss10SlashExtensionConstants.Namespace + "comments") { Value = valueString };

            return true;
        }

        private bool TryFormatRss10SlashHitParade(Rss10SlashHitParade entity, out XElement element)
        {
            element = default;

            if (entity?.Identifiers?.Any() != true)
                return false;

            var valueString = string.Join(",", entity.Identifiers.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            element = new XElement(Rss10SlashExtensionConstants.Namespace + "hit_parade") { Value = valueString };

            return true;
        }
    }
}