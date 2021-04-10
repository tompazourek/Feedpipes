using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Extensions.Rss10Slash.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.Rss10Slash
{
    /// <summary>
    /// Optional "slash:*" extended information.
    /// </summary>
    public class Rss10SlashExtensionManifest : ExtensionManifest<Rss10SlashExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out Rss10SlashExtension extension)
            => Rss10SlashExtensionParser.TryParseRss10SlashExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(Rss10SlashExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            => Rss10SlashExtensionFormatter.TryFormatRss10SlashExtension(extensionToFormat, namespaceAliases, out elements);
    }
}
