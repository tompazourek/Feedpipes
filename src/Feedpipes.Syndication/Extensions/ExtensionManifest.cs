using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions
{
    public abstract class ExtensionManifest
    {
        public abstract Type ExtensionType { get; }
        public abstract bool TryParseXElementExtension(XElement parentElement, out IExtensionEntity extension);
        public abstract bool TryFormatXElementExtension(IExtensionEntity extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements);
    }

    public abstract class ExtensionManifest<TExtension> : ExtensionManifest
        where TExtension : IExtensionEntity, new()
    {
        public override Type ExtensionType => typeof(TExtension);

        public override bool TryParseXElementExtension(XElement parentElement, out IExtensionEntity extension)
        {
            var result = TryParseXElementExtension(parentElement, out var typedExtension);
            extension = typedExtension;
            return result;
        }

        public override bool TryFormatXElementExtension(IExtensionEntity extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            if (extensionToFormat is TExtension typedExtension)
                return TryFormatXElementExtension(typedExtension, namespaceAliases, out elements);

            elements = default;
            return false;
        }

        protected abstract bool TryParseXElementExtension(XElement parentElement, out TExtension extension);
        protected abstract bool TryFormatXElementExtension(TExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements);
    }
}