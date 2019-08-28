using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication
{
    /// <summary>
    /// Optional "sy:*" extended information.
    /// </summary>
    public class Rss10SyndicationExtensionManifest : ExtensionManifest<Rss10SyndicationExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out Rss10SyndicationExtension extension)
            => Rss10SyndicationExtensionParser.TryParseRss10SyndicationExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(Rss10SyndicationExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => Rss10SyndicationExtensionFormatter.TryFormatRss10SyndicationExtension(extensionToFormat, namespaceAliases, out elements);
    }
}