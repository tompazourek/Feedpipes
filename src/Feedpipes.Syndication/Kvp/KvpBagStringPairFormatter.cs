using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Feedpipes.Syndication.Kvp
{
    public static class KvpBagStringPairFormatter
    {
        public static bool TryFormatKvpBag(KvpBag kvpBag, out IReadOnlyDictionary<string, string> stringPairs)
        {
            stringPairs = default;

            var result = new Dictionary<string, string>();
            foreach (var kvpPair in kvpBag)
            {
                if (!TryFormatKvpKey(kvpPair.Key, out var formattedKey))
                    return false;

                if (!result.TryAdd(formattedKey, kvpPair.Value.Value))
                    return false;
            }

            stringPairs = result;
            return true;
        }

        internal static bool TryFormatKvpKey(KvpBagKey kvpBagKey, out string formattedKey)
        {
            formattedKey = default;

            var resultBuilder = new StringBuilder();

            string parentNamespaceIdentifier = null;
            foreach (var part in kvpBagKey.Parts)
            {
                if (!TryFormatKvpKeyPart(part, out var formattedPart, parentNamespaceIdentifier))
                {
                    return false;
                }

                if (resultBuilder.Length > 0)
                {
                    resultBuilder.Append('.');
                }

                resultBuilder.Append(formattedPart);
                parentNamespaceIdentifier = part.NamespaceIdentifier;
            }

            formattedKey = resultBuilder.ToString();
            return true;
        }

        internal static bool TryFormatKvpKeyPart(KvpBagKeyPart kvpBagKeyPart, out string formattedPart, string parentNamespaceIdentifier = null)
        {
            var resultBuilder = new StringBuilder();

            if (kvpBagKeyPart.NamespaceIdentifier != parentNamespaceIdentifier)
            {
                resultBuilder.Append(kvpBagKeyPart.NamespaceIdentifier + ":");
            }

            resultBuilder.Append(kvpBagKeyPart.PropertyName);

            if (kvpBagKeyPart.CollectionIndex != null)
            {
                resultBuilder.Append('[');
                resultBuilder.Append(kvpBagKeyPart.CollectionIndex.Value.ToString(CultureInfo.InvariantCulture));
                resultBuilder.Append(']');
            }

            formattedPart = resultBuilder.ToString();
            return true;
        }
    }
}