using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    /// <summary>
    /// Optional "wfw:*" extended information.
    /// </summary>
    public class WellFormedWebExtensionManifest : ExtensionManifest<WellFormedWebExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out WellFormedWebExtension extension)
            => WellFormedWebExtensionParser.TryParseWellFormedWebExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(WellFormedWebExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => WellFormedWebExtensionFormatter.TryFormatWellFormedWebExtension(extensionToFormat, namespaceAliases, out elements);
    }
}