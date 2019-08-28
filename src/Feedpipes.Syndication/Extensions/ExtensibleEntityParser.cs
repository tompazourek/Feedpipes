using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace Feedpipes.Syndication.Extensions
{
    internal static class ExtensibleEntityParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        [SuppressMessage("ReSharper", "InvertIf")]
        public static void ParseExtensibleEntityExtensions<T>([NotNull] XElement parentElement, [NotNull] ExtensionManifestDirectory extensionManifestDirectory, [NotNull] T entityToExtend)
            where T : IExtensibleEntity
        {
            if (parentElement == null) throw new ArgumentNullException(nameof(parentElement));
            if (extensionManifestDirectory == null) throw new ArgumentNullException(nameof(extensionManifestDirectory));
            if (entityToExtend == null) throw new ArgumentNullException(nameof(entityToExtend));

            foreach (var extensionManifest in extensionManifestDirectory)
            {
                if (extensionManifest.TryParseXElementExtension(parentElement, out var extension))
                {
                    entityToExtend.Extensions.Add(extension);
                }
            }
        }
    }
}