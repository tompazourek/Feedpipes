using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    public class Rss10ContentExtensionFormatter : IFeedExtensionFormatter
    {
        public string GetNamespaceAlias() => Rss10ContentExtensionConstants.NamespaceAlias;
        public XNamespace GetNamespace() => Rss10ContentExtensionConstants.Namespace;

        public bool TryFormatExtensionEntity(IFeedExtensionEntity extensionEntityToFormat, out XElement element)
        {
            element = default;

            if (extensionEntityToFormat == null)
                return false;

            switch (extensionEntityToFormat)
            {
                case Rss10ContentEncoded entity:
                    if (TryFormatRss10ContentEncoded(entity, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatRss10ContentEncoded(Rss10ContentEncoded entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(Rss10ContentExtensionConstants.Namespace + "encoded");
            element.Add(new XCData(entity.Content));

            return true;
        }
    }
}