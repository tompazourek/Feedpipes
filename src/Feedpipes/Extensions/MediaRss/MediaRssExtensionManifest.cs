﻿using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Extensions.MediaRss.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.MediaRss
{
    /// <summary>
    /// Optional "media:*" extended information.
    /// </summary>
    public class MediaRssExtensionManifest : ExtensionManifest<MediaRssExtension>
    {
        protected override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out MediaRssExtension extension)
            => MediaRssExtensionParser.TryParseMediaRssExtension(parentElement, extensionManifestDirectory, out extension);

        protected override bool TryFormatXElementExtension(MediaRssExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            => MediaRssExtensionFormatter.TryFormatMediaRssExtension(extensionToFormat, namespaceAliases, extensionManifestDirectory, out elements);
    }
}
