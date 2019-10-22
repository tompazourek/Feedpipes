using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Extensions.CreativeCommons.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.CreativeCommons
{
    /// <summary>
    /// Optional "cc:*" extended information.
    /// </summary>
    public class CreativeCommonsExtensionManifest : ExtensionManifest<CreativeCommonsExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out CreativeCommonsExtension extension)
            => CreativeCommonsExtensionParser.TryParseCreativeCommonsExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(CreativeCommonsExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            => CreativeCommonsExtensionFormatter.TryFormatCreativeCommonsExtension(extensionToFormat, namespaceAliases, out elements);
    }
}