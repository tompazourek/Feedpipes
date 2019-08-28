using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions
{
    internal static class ExtensibleEntityFormatter
    {
        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        public static bool TryFormatExtensibleEntityExtensions<T>(T entityToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out IList<XElement> elements)
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

                if (extensionManifest.TryFormatXElementExtension(extensionEntity, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (!results.Any())
                return false;

            elements = results;

            return true;
        }
    }
}