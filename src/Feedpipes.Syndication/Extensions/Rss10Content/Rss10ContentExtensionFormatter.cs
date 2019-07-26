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
                // format "content:encoded"
                case Rss10ContentEncoded encodedToFormat:
                    if (TryFormatRss10ContentEncoded(encodedToFormat, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatRss10ContentEncoded(Rss10ContentEncoded encodedToFormat, out XElement encodedElement)
        {
            encodedElement = default;

            if (encodedToFormat == null)
                return false;

            encodedElement = new XElement(Rss10ContentExtensionConstants.Namespace + "encoded");
            encodedElement.Add(new XCData(encodedToFormat.Content));

            return true;
        }
    }
}