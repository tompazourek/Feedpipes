using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Extensions.Rss10Syndication.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.Rss10Syndication
{
    /// <summary>
    /// Optional "sy:*" extended information.
    /// </summary>
    public class Rss10SyndicationExtensionManifest : ExtensionManifest<Rss10SyndicationExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out Rss10SyndicationExtension extension)
            => Rss10SyndicationExtensionParser.TryParseRss10SyndicationExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(Rss10SyndicationExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            => Rss10SyndicationExtensionFormatter.TryFormatRss10SyndicationExtension(extensionToFormat, namespaceAliases, out elements);
    }
}