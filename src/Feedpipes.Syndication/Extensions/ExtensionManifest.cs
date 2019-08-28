using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Utils.Xml;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Syndication.Extensions
{
    public abstract class ExtensionManifest
    {
        public abstract Type ExtensionType { get; }

        #region XML

        public abstract bool TryParseXElementExtension(XElement parentElement, out IExtensionEntity extension);
        public abstract bool TryFormatXElementExtension(IExtensionEntity extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements);

        #endregion

        #region JSON

        public abstract bool TryParseJObjectExtension(JObject parentObject, out IExtensionEntity extension);
        public abstract bool TryFormatJObjectExtension(IExtensionEntity extensionToFormat, out IList<JToken> tokens);

        #endregion
    }

    public abstract class ExtensionManifest<TExtension> : ExtensionManifest
        where TExtension : IExtensionEntity, new()
    {
        public override Type ExtensionType => typeof(TExtension);

        #region XML

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

        protected virtual bool TryParseXElementExtension(XElement parentElement, out TExtension extension)
        {
            extension = default;
            return false;
        }

        protected virtual bool TryFormatXElementExtension(TExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;
            return false;
        }

        #endregion

        #region JSON

        public override bool TryParseJObjectExtension(JObject parentObject, out IExtensionEntity extension)
        {
            var result = TryParseJObjectExtension(parentObject, out var typedExtension);
            extension = typedExtension;
            return result;
        }

        public override bool TryFormatJObjectExtension(IExtensionEntity extensionToFormat, out IList<JToken> tokens)
        {
            if (extensionToFormat is TExtension typedExtension)
                return TryFormatJObjectExtension(typedExtension, out tokens);

            tokens = default;
            return false;
        }

        protected virtual bool TryParseJObjectExtension(JObject parentObject, out TExtension extension)
        {
            extension = default;
            return false;
        }

        protected virtual bool TryFormatJObjectExtension(TExtension extensionToFormat, out IList<JToken> tokens)
        {
            tokens = default;
            return false;
        }

        #endregion
    }
}