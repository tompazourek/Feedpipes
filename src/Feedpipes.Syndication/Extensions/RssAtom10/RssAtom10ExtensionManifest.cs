using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.RssAtom10.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.RssAtom10
{
    /// <summary>
    /// Optional "atom10:*" extended information.
    /// </summary>
    public class RssAtom10ExtensionManifest : ExtensionManifest<RssAtom10Extension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out RssAtom10Extension extension)
            => RssAtom10ExtensionParser.TryParseRssAtom10Extension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(RssAtom10Extension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => RssAtom10ExtensionFormatter.TryFormatRssAtom10Extension(extensionToFormat, namespaceAliases, out elements);
    }
}