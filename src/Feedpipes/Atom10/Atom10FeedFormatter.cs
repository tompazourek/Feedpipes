using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Feedpipes.Atom10.Entities;
using Feedpipes.Extensions;
using Feedpipes.Timestamps.Rfc3339;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Atom10
{
    public static class Atom10FeedFormatter
    {
        private static readonly XNamespace _atom = Atom10Constants.Namespace;
        private static readonly XNamespace _xml = XNamespace.Xml;

        public static bool TryFormatAtom10Feed(Atom10Feed feed, out XDocument document, ExtensionManifestDirectory extensionManifestDirectory = null)
        {
            document = default;

            if (feed == null)
                return false;

            document = new XDocument();

            var feedElement = new XElement(_atom + "feed");
            document.Add(feedElement);

            var namespaceAliases = new XNamespaceAliasSet();
            namespaceAliases.EnsureNamespaceAlias(alias: null, _atom);

            if (extensionManifestDirectory == null)
            {
                extensionManifestDirectory = ExtensionManifestDirectory.DefaultForAtom;
            }

            if (TryFormatAtom10OptionalTextAttribute(feed.Lang, _xml + "lang", out var langAttribute))
            {
                feedElement.Add(langAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(feed.Base, _xml + "base", out var baseAttribute))
            {
                feedElement.Add(baseAttribute);
            }

            if (TryFormatAtom10RequiredTextElement(feed.Id, _atom + "id", out var idElement))
            {
                feedElement.Add(idElement);
            }

            if (TryFormatAtom10TextRequired(feed.Title, _atom + "title", out var titleElement))
            {
                feedElement.Add(titleElement);
            }

            if (TryFormatAtom10TimestampRequired(feed.Updated, _atom + "updated", out var updatedElement))
            {
                feedElement.Add(updatedElement);
            }

            foreach (var authorToFormat in feed.Authors)
            {
                if (TryFormatAtom10Person(authorToFormat, _atom + "author", out var authorElement))
                {
                    feedElement.Add(authorElement);
                }
            }

            foreach (var linkToFormat in feed.Links)
            {
                if (TryFormatAtom10Link(linkToFormat, out var linkElement))
                {
                    feedElement.Add(linkElement);
                }
            }

            foreach (var categoryToFormat in feed.Categories)
            {
                if (TryFormatAtom10Category(categoryToFormat, out var categoryElement))
                {
                    feedElement.Add(categoryElement);
                }
            }

            foreach (var contributorToFormat in feed.Contributors)
            {
                if (TryFormatAtom10Person(contributorToFormat, _atom + "contributor", out var contributorElement))
                {
                    feedElement.Add(contributorElement);
                }
            }

            if (TryFormatAtom10Generator(feed.Generator, out var generatorElement))
            {
                feedElement.Add(generatorElement);
            }

            if (TryFormatAtom10OptionalTextElement(feed.Icon, _atom + "icon", out var iconElement))
            {
                feedElement.Add(iconElement);
            }

            if (TryFormatAtom10OptionalTextElement(feed.Logo, _atom + "logo", out var logoElement))
            {
                feedElement.Add(logoElement);
            }

            if (TryFormatAtom10TextOptional(feed.Rights, _atom + "rights", out var rightsElement))
            {
                feedElement.Add(rightsElement);
            }

            if (TryFormatAtom10TextOptional(feed.Subtitle, _atom + "subtitle", out var subtitleElement))
            {
                feedElement.Add(subtitleElement);
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(feed, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                feedElement.AddRange(extensionElements);
            }

            // entries
            foreach (var entryToFormat in feed.Entries)
            {
                if (TryFormatAtom10Entry(entryToFormat, namespaceAliases, extensionManifestDirectory, out var entryElement))
                {
                    feedElement.Add(entryElement);
                }
            }

            foreach (var namespaceAlias in namespaceAliases.OrderBy(x => x.Name.LocalName))
            {
                feedElement.Add(namespaceAlias);
            }

            return true;
        }

        private static bool TryFormatAtom10Entry(Atom10Entry entryToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement entryElement)
        {
            entryElement = default;

            if (entryToFormat == null)
                return false;

            entryElement = new XElement(_atom + "entry");

            if (TryFormatAtom10RequiredTextElement(entryToFormat.Id, _atom + "id", out var idElement))
            {
                entryElement.Add(idElement);
            }

            if (TryFormatAtom10TextRequired(entryToFormat.Title, _atom + "title", out var titleElement))
            {
                entryElement.Add(titleElement);
            }

            if (TryFormatAtom10TimestampRequired(entryToFormat.Updated, _atom + "updated", out var updatedElement))
            {
                entryElement.Add(updatedElement);
            }

            foreach (var authorToFormat in entryToFormat.Authors)
            {
                if (TryFormatAtom10Person(authorToFormat, _atom + "author", out var authorElement))
                {
                    entryElement.Add(authorElement);
                }
            }

            if (TryFormatAtom10Content(entryToFormat.Content, out var contentElement))
            {
                entryElement.Add(contentElement);
            }

            foreach (var linkToFormat in entryToFormat.Links)
            {
                if (TryFormatAtom10Link(linkToFormat, out var linkElement))
                {
                    entryElement.Add(linkElement);
                }
            }

            if (TryFormatAtom10TextOptional(entryToFormat.Summary, _atom + "summary", out var summaryElement))
            {
                entryElement.Add(summaryElement);
            }

            foreach (var categoryToFormat in entryToFormat.Categories)
            {
                if (TryFormatAtom10Category(categoryToFormat, out var categoryElement))
                {
                    entryElement.Add(categoryElement);
                }
            }

            foreach (var contributorToFormat in entryToFormat.Contributors)
            {
                if (TryFormatAtom10Person(contributorToFormat, _atom + "contributor", out var contributorElement))
                {
                    entryElement.Add(contributorElement);
                }
            }

            if (TryFormatAtom10TimestampOptional(entryToFormat.Published, _atom + "published", out var publishedElement))
            {
                entryElement.Add(publishedElement);
            }

            if (TryFormatAtom10TextOptional(entryToFormat.Rights, _atom + "rights", out var rightsElement))
            {
                entryElement.Add(rightsElement);
            }

            if (TryFormatAtom10Source(entryToFormat.Source, out var sourceElement))
            {
                entryElement.Add(sourceElement);
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(entryToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                entryElement.AddRange(extensionElements);
            }

            return true;
        }

        private static bool TryFormatAtom10TimestampRequired(DateTimeOffset? timestampToFormat, XName name, out XElement timestampElement)
        {
            string timestampString;

            if (timestampToFormat == null)
            {
                timestampString = "";
            }
            else if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(timestampToFormat.Value, out timestampString))
            {
                timestampString = "";
            }

            timestampElement = new XElement(name, timestampString);
            return true;
        }

        private static bool TryFormatAtom10TimestampOptional(DateTimeOffset? timestampToFormat, XName name, out XElement timestampElement)
        {
            timestampElement = default;

            if (timestampToFormat == null)
                return false;

            if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(timestampToFormat.Value, out var timestampString))
                return false;

            timestampElement = new XElement(name, timestampString);
            return true;
        }

        private static bool TryFormatAtom10TextRequired(Atom10Text textToFormat, XName name, out XElement textElement)
        {
            textElement = new XElement(name);

            if (TryFormatValueByType(textToFormat?.Type, textToFormat?.Value, out var contentObject))
            {
                textElement.Add(contentObject);
            }

            if (TryFormatAtom10Type(textToFormat?.Type, skipTypeText: true, out var typeAttribute))
            {
                textElement.Add(typeAttribute);
            }
            
            if (TryFormatAtom10OptionalTextAttribute(textToFormat?.Lang, _xml + "lang", out var langAttribute))
            {
                textElement.Add(langAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(textToFormat?.Base, _xml + "base", out var baseAttribute))
            {
                textElement.Add(baseAttribute);
            }

            return true;
        }

        private static bool TryFormatAtom10TextOptional(Atom10Text textToFormat, XName name, out XElement textElement)
        {
            textElement = default;

            if (string.IsNullOrEmpty(textToFormat?.Value))
                return false;

            textElement = new XElement(name);
            
            if (TryFormatValueByType(textToFormat.Type, textToFormat.Value, out var contentObject))
            {
                textElement.Add(contentObject);
            }

            if (TryFormatAtom10Type(textToFormat.Type, skipTypeText: true, out var typeAttribute))
            {
                textElement.Add(typeAttribute);
            }
            
            if (TryFormatAtom10OptionalTextAttribute(textToFormat.Lang, _xml + "lang", out var langAttribute))
            {
                textElement.Add(langAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(textToFormat.Base, _xml + "base", out var baseAttribute))
            {
                textElement.Add(baseAttribute);
            }

            return true;
        }

        private static bool TryFormatAtom10Type(string typeToFormat, bool skipTypeText, out XAttribute typeAttribute)
        {
            typeAttribute = default;

            if (string.IsNullOrWhiteSpace(typeToFormat))
                return false;

            if (skipTypeText && typeToFormat == "text")
                return false;

            typeAttribute = new XAttribute("type", typeToFormat);
            return true;
        }

        private static bool TryFormatAtom10RequiredTextElement(string valueToFormat, XName name, out XElement element)
        {
            element = new XElement(name, valueToFormat ?? "");
            return true;
        }

        private static bool TryFormatAtom10OptionalTextElement(string valueToFormat, XName name, out XElement element)
        {
            element = default;

            if (string.IsNullOrEmpty(valueToFormat))
                return false;

            element = new XElement(name, valueToFormat);
            return true;
        }

        private static bool TryFormatAtom10OptionalTextAttribute(string valueToFormat, XName name, out XAttribute attribute)
        {
            attribute = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            attribute = new XAttribute(name, valueToFormat);
            return true;
        }

        private static bool TryFormatAtom10OptionalNumericAttribute(int? valueToFormat, XName name, out XAttribute attribute)
        {
            attribute = default;

            if (valueToFormat == null)
                return false;

            var valueString = valueToFormat.Value.ToString(CultureInfo.InvariantCulture);
            attribute = new XAttribute(name, valueString);
            return true;
        }

        private static bool TryFormatAtom10Source(Atom10Source sourceToFormat, out XElement sourceElement)
        {
            sourceElement = default;

            if (sourceToFormat == null)
                return false;

            sourceElement = new XElement(_atom + "source");

            if (TryFormatAtom10OptionalTextElement(sourceToFormat.Id, _atom + "id", out var idElement))
            {
                sourceElement.Add(idElement);
            }

            if (TryFormatAtom10TextOptional(sourceToFormat.Title, _atom + "title", out var titleElement))
            {
                sourceElement.Add(titleElement);
            }

            if (TryFormatAtom10TimestampOptional(sourceToFormat.Updated, _atom + "updated", out var updatedElement))
            {
                sourceElement.Add(updatedElement);
            }

            return sourceElement.HasElements;
        }

        private static bool TryFormatAtom10Content(Atom10Content contentToFormat, out XElement contentElement)
        {
            contentElement = default;

            if (string.IsNullOrEmpty(contentToFormat?.Value))
                return false;

            contentElement = new XElement(_atom + "content");

            if (TryFormatValueByType(contentToFormat.Type, contentToFormat.Value, out var contentObject))
            {
                contentElement.Add(contentObject);
            }

            if (TryFormatAtom10Type(contentToFormat.Type, skipTypeText: false, out var typeAttribute))
            {
                contentElement.Add(typeAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(contentToFormat.Src, "src", out var srcAttribute))
            {
                contentElement.Add(srcAttribute);
            }
            
            if (TryFormatAtom10OptionalTextAttribute(contentToFormat.Lang, _xml + "lang", out var langAttribute))
            {
                contentElement.Add(langAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(contentToFormat.Base, _xml + "base", out var baseAttribute))
            {
                contentElement.Add(baseAttribute);
            }

            return true;
        }
        
        private static bool TryFormatValueByType(string type, string valueToFormat, out object content)
        {
            content = default;
            
            if (string.IsNullOrEmpty(valueToFormat))
                return false;

            switch (type)
            {
                case "xhtml":
                    try
                    {
                        content = XElement.Parse(valueToFormat);
                        return true;
                    }
                    catch (XmlException)
                    {
                        return false;
                    }
                default:
                    content = valueToFormat;
                    return true;
            }
        }

        private static bool TryFormatAtom10Generator(Atom10Generator generatorToFormat, out XElement generatorElement)
        {
            generatorElement = default;

            if (string.IsNullOrEmpty(generatorToFormat?.Value))
                return false;

            generatorElement = new XElement(_atom + "generator", generatorToFormat.Value);

            if (TryFormatAtom10OptionalTextAttribute(generatorToFormat.Uri, "uri", out var uriAttribute))
            {
                generatorElement.Add(uriAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(generatorToFormat.Version, "version", out var versionAttribute))
            {
                generatorElement.Add(versionAttribute);
            }

            return true;
        }

        private static bool TryFormatAtom10Category(Atom10Category categoryToFormat, out XElement categoryElement)
        {
            categoryElement = default;

            if (string.IsNullOrEmpty(categoryToFormat?.Term))
                return false;

            categoryElement = new XElement(_atom + "category");

            categoryElement.Add(new XAttribute("term", categoryToFormat.Term));

            if (TryFormatAtom10OptionalTextAttribute(categoryToFormat.Label, "label", out var labelAttribute))
            {
                categoryElement.Add(labelAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(categoryToFormat.Scheme, "scheme", out var schemeAttribute))
            {
                categoryElement.Add(schemeAttribute);
            }

            return true;
        }

        private static bool TryFormatAtom10Link(Atom10Link linkToFormat, out XElement linkElement)
        {
            linkElement = default;

            if (string.IsNullOrEmpty(linkToFormat.Href))
                return false;

            linkElement = new XElement(_atom + "link");

            linkElement.Add(new XAttribute("href", linkToFormat.Href));

            if (TryFormatAtom10OptionalTextAttribute(linkToFormat.Rel, "rel", out var relAttribute))
            {
                linkElement.Add(relAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(linkToFormat.Type, "type", out var typeAttribute))
            {
                linkElement.Add(typeAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(linkToFormat.Hreflang, "hreflang", out var hreflangAttribute))
            {
                linkElement.Add(hreflangAttribute);
            }

            if (TryFormatAtom10OptionalTextAttribute(linkToFormat.Title, "title", out var titleAttribute))
            {
                linkElement.Add(titleAttribute);
            }

            if (TryFormatAtom10OptionalNumericAttribute(linkToFormat.Length, "length", out var lengthAttribute))
            {
                linkElement.Add(lengthAttribute);
            }

            return true;
        }

        private static bool TryFormatAtom10Person(Atom10Person personToFormat, XName name, out XElement personElement)
        {
            personElement = default;

            if (string.IsNullOrEmpty(personToFormat?.Name))
                return false;

            personElement = new XElement(name);

            personElement.Add(new XElement(_atom + "name", personToFormat.Name));

            if (TryFormatAtom10OptionalTextElement(personToFormat.Uri, _atom + "uri", out var uriAttribute))
            {
                personElement.Add(uriAttribute);
            }

            if (TryFormatAtom10OptionalTextElement(personToFormat.Email, _atom + "email", out var emailAttribute))
            {
                personElement.Add(emailAttribute);
            }

            return true;
        }
    }
}