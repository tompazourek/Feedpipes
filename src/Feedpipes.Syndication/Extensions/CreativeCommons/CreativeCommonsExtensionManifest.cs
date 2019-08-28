using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.CreativeCommons
{
    /// <summary>
    /// Optional "cc:*" extended information.
    /// </summary>
    public class CreativeCommonsExtensionManifest : ExtensionManifest<CreativeCommonsExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, out CreativeCommonsExtension extension)
            => CreativeCommonsExtensionParser.TryParseCreativeCommonsExtension(parentElement, out extension);

        protected override bool TryFormatXElementExtension(CreativeCommonsExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            => CreativeCommonsExtensionFormatter.TryFormatCreativeCommonsExtension(extensionToFormat, namespaceAliases, out elements);
    }
}