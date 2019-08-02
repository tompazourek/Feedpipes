using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;

namespace Feedpipes.Syndication.Extensions.CreativeCommons
{
    internal static class CreativeCommonsElementExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseCreativeCommonsElementExtension(XElement parentElement, out CreativeCommonsElementExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            foreach (var ns in CreativeCommonsConstants.RecognizedNamespaces)
            {
                foreach (var licenseElement in parentElement.Elements(ns + "license"))
                {
                    if (TryParseCreativeCommonsLicenseElement(licenseElement, out var parsedLicense))
                    {
                        extension = extension ?? new CreativeCommonsElementExtension();
                        extension.Licenses.Add(parsedLicense);
                    }
                }
            }

            return extension != null;
        }

        private static bool TryParseCreativeCommonsLicenseElement(XElement licenseElement, out CreativeCommonsLicense parsedLicense)
        {
            parsedLicense = default;

            if (licenseElement == null)
                return false;

            var licenseValue = licenseElement.Value.Trim();

            if (string.IsNullOrEmpty(licenseValue))
                return false;

            parsedLicense = new CreativeCommonsLicense { Value = licenseValue };
            return true;
        }
    }
}