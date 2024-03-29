﻿using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Extensions
{
    internal static class ExtensibleEntityParser
    {
        public static void ParseXElementExtensions<T>(XElement parentElement, ExtensionManifestDirectory extensionManifestDirectory, T entityToExtend)
            where T : IExtensibleEntity
        {
            if (parentElement == null) throw new ArgumentNullException(nameof(parentElement));
            if (extensionManifestDirectory == null) throw new ArgumentNullException(nameof(extensionManifestDirectory));
            if (entityToExtend == null) throw new ArgumentNullException(nameof(entityToExtend));

            foreach (var extensionManifest in extensionManifestDirectory)
            {
                if (extensionManifest.TryParseXElementExtension(parentElement, extensionManifestDirectory, out var extension))
                {
                    entityToExtend.Extensions.Add(extension);
                }
            }
        }

        public static void ParseJObjectExtensions<T>(JObject parentObject, ExtensionManifestDirectory extensionManifestDirectory, T entityToExtend)
            where T : IExtensibleEntity
        {
            if (parentObject == null) throw new ArgumentNullException(nameof(parentObject));
            if (extensionManifestDirectory == null) throw new ArgumentNullException(nameof(extensionManifestDirectory));
            if (entityToExtend == null) throw new ArgumentNullException(nameof(entityToExtend));

            foreach (var extensionManifest in extensionManifestDirectory)
            {
                if (extensionManifest.TryParseJObjectExtension(parentObject, extensionManifestDirectory, out var extension))
                {
                    entityToExtend.Extensions.Add(extension);
                }
            }
        }
    }
}
