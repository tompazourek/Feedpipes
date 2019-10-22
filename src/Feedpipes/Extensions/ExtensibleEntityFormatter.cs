using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Utils.Xml;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Extensions
{
    internal static class ExtensibleEntityFormatter
    {
        public static bool TryFormatXElementExtensions<T>(T entityToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
            where T : IExtensibleEntity
        {
            elements = default;

            if (entityToFormat == null)
                return false;

            var results = new List<XElement>();

            foreach (var extensionEntity in entityToFormat.Extensions)
            {
                if (!extensionManifestDirectory.TryGetExtensionManifestByExtensionType(extensionEntity.GetType(), out var extensionManifest))
                    continue;

                if (extensionManifest.TryFormatXElementExtension(extensionEntity, namespaceAliases, extensionManifestDirectory, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (!results.Any())
                return false;

            elements = results;

            return true;
        }

        public static bool TryFormatJObjectExtensions<T>(T entityToFormat, ExtensionManifestDirectory extensionManifestDirectory, out IList<JToken> tokens)
            where T : IExtensibleEntity
        {
            tokens = default;

            if (entityToFormat == null)
                return false;

            var results = new List<JToken>();

            foreach (var extensionEntity in entityToFormat.Extensions)
            {
                if (!extensionManifestDirectory.TryGetExtensionManifestByExtensionType(extensionEntity.GetType(), out var extensionManifest))
                    continue;

                if (extensionManifest.TryFormatJObjectExtension(extensionEntity, extensionManifestDirectory, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (!results.Any())
                return false;

            tokens = results;

            return true;
        }
    }
}