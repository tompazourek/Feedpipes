using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.DublinCore
{
    /// <summary>
    /// Optional "dc:*" extended information.
    /// </summary>
    public class DublinCoreExtensionManifest : ExtensionManifest<DublinCoreExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out DublinCoreExtension extension)
            => DublinCoreExtensionParser.TryParseDublinCoreExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(DublinCoreExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => DublinCoreExtensionFormatter.TryFormatDublinCoreExtension(extensionToFormat, namespaceAliases, out elements);
    }
}