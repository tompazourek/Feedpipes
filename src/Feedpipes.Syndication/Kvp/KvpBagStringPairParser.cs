using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Feedpipes.Syndication.Kvp
{
    public static class KvpBagStringPairParser
    {
        private static readonly Regex KvpBagKeyPartRegex = new Regex(@"^((?<NamespaceIdentifier>[a-z]+):)?(?<PropertyName>[a-z]+)(\[(?<CollectionIndex>[0-9]+)\])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static bool TryParseKvpBag(IReadOnlyDictionary<string, string> stringPairs, out KvpBag kvpBag)
        {
            kvpBag = new KvpBag();

            foreach (var (key, value) in stringPairs)
            {
                if (!TryParseKvpBagKey(key, out var kvpBagKey))
                    return false;

                if (!TryParseKvpBagValue(value, out var kvpBagValue))
                    return false;

                if (!kvpBag.TryAdd(kvpBagKey, kvpBagValue))
                    return false;
            }

            return true;
        }

        private static bool TryParseKvpBagKey(string input, out KvpBagKey kvpBagKey)
        {
            kvpBagKey = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var inputParts = input.Split('.');
            var resultParts = new List<KvpBagKeyPart>();

            string parentNamespaceIdentifier = null;

            foreach (var inputPart in inputParts)
            {
                if (!TryParseKvpBagKeyPart(inputPart, out var resultPart, parentNamespaceIdentifier))
                    return false;

                resultParts.Add(resultPart);
                parentNamespaceIdentifier = resultPart.NamespaceIdentifier;
            }

            kvpBagKey = new KvpBagKey(resultParts);
            return true;
        }

        private static bool TryParseKvpBagKeyPart(string input, out KvpBagKeyPart kvpBagKeyPart, string parentNamespaceIdentifier = null)
        {
            kvpBagKeyPart = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var match = KvpBagKeyPartRegex.Match(input);
            if (!match.Success)
                return false;

            var namespaceIdentifier = match.Groups["NamespaceIdentifier"].Success ? match.Groups["NamespaceIdentifier"].Value : null;
            if (string.IsNullOrWhiteSpace(namespaceIdentifier))
            {
                namespaceIdentifier = parentNamespaceIdentifier;
            }

            if (string.IsNullOrWhiteSpace(namespaceIdentifier))
                return false;

            var propertyName = match.Groups["PropertyName"].Value;
            var collectionIndex = match.Groups["CollectionIndex"].Success ? int.Parse(match.Groups["CollectionIndex"].Value) : (int?) null;

            kvpBagKeyPart = new KvpBagKeyPart(namespaceIdentifier, propertyName, collectionIndex);
            return true;
        }

        private static bool TryParseKvpBagValue(string input, out KvpBagValue kvpBagValue)
        {
            kvpBagValue = default;

            if (string.IsNullOrEmpty(input))
                return false;

            kvpBagValue = new KvpBagValue(input);
            return true;
        }
    }
}