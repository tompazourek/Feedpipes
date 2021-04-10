using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Utils.Xml;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Extensions
{
    public abstract class ExtensionManifest
    {
        public abstract Type ExtensionType { get; }

        #region XML

        public abstract bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out IExtensionEntity extension);
        public abstract bool TryFormatXElementExtension(IExtensionEntity extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements);

        #endregion

        #region JSON

        public abstract bool TryParseJObjectExtension(JObject parentObject, ExtensionManifestDirectory extensionManifestDirectory, out IExtensionEntity extension);
        public abstract bool TryFormatJObjectExtension(IExtensionEntity extensionToFormat, ExtensionManifestDirectory extensionManifestDirectory, out IList<JToken> tokens);

        #endregion
    }

    public abstract class ExtensionManifest<TExtension> : ExtensionManifest
        where TExtension : IExtensionEntity, new()
    {
        public override Type ExtensionType => typeof(TExtension);

        #region XML

        public override bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out IExtensionEntity extension)
        {
            var result = TryParseXElementExtension(parentElement, extensionManifestDirectory, out var typedExtension);
            extension = typedExtension;
            return result;
        }

        public override bool TryFormatXElementExtension(IExtensionEntity extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
        {
            if (extensionToFormat is TExtension typedExtension)
                return TryFormatXElementExtension(typedExtension, namespaceAliases, extensionManifestDirectory, out elements);

            elements = default;
            return false;
        }

        protected virtual bool TryParseXElementExtension(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, out TExtension extension)
        {
            extension = default;
            return false;
        }

        protected virtual bool TryFormatXElementExtension(TExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
        {
            elements = default;
            return false;
        }

        #endregion

        #region JSON

        public override bool TryParseJObjectExtension(JObject parentObject, ExtensionManifestDirectory extensionManifestDirectory, out IExtensionEntity extension)
        {
            var result = TryParseJObjectExtension(parentObject, extensionManifestDirectory, out var typedExtension);
            extension = typedExtension;
            return result;
        }

        public override bool TryFormatJObjectExtension(IExtensionEntity extensionToFormat, ExtensionManifestDirectory extensionManifestDirectory, out IList<JToken> tokens)
        {
            if (extensionToFormat is TExtension typedExtension)
                return TryFormatJObjectExtension(typedExtension, extensionManifestDirectory, out tokens);

            tokens = default;
            return false;
        }

        protected virtual bool TryParseJObjectExtension(JObject parentObject, ExtensionManifestDirectory extensionManifestDirectory, out TExtension extension)
        {
            extension = default;
            return false;
        }

        protected virtual bool TryFormatJObjectExtension(TExtension extensionToFormat, ExtensionManifestDirectory extensionManifestDirectory, out IList<JToken> tokens)
        {
            tokens = default;
            return false;
        }

        #endregion
    }
}
