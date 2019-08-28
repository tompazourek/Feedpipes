using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    /// <summary>
    /// Optional "content:*" extended information.
    /// </summary>
    public class Rss10ContentExtensionManifest : ExtensionManifest<Rss10ContentExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out Rss10ContentExtension extension)
            => Rss10ContentExtensionParser.TryParseRss10ContentExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(Rss10ContentExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => Rss10ContentExtensionFormatter.TryFormatRss10ContentExtension(extensionToFormat, namespaceAliases, out elements);
    }
}