using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Extensions.DublinCore.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.DublinCore
{
    /// <summary>
    /// Optional "dc:*" extended information.
    /// </summary>
    public class DublinCoreExtensionManifest : ExtensionManifest<DublinCoreExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out DublinCoreExtension extension)
            => DublinCoreExtensionParser.TryParseDublinCoreExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(DublinCoreExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            => DublinCoreExtensionFormatter.TryFormatDublinCoreExtension(extensionToFormat, namespaceAliases, out elements);
    }
}
