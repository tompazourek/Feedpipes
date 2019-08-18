using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Feedpipes.Syndication.Kvp
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class KvpBagKeyPart : IEquatable<KvpBagKeyPart>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => KvpBagStringPairFormatter.TryFormatKvpKeyPart(this, out var result) ? result : null;

        public override string ToString() => DebuggerDisplay;

        public KvpBagKeyPart([NotNull] string namespaceIdentifier, [NotNull] string propertyName, int? collectionIndex = null)
        {
            if (string.IsNullOrWhiteSpace(namespaceIdentifier))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(namespaceIdentifier));

            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyName));

            if (collectionIndex < 0)
                throw new ArgumentException("Collection index cannot be a negative number.", nameof(collectionIndex));

            if (namespaceIdentifier.ToLowerInvariant() != namespaceIdentifier)
                throw new ArgumentException($"Namespace identifier must be a lowercase string, '{namespaceIdentifier}' given.", nameof(namespaceIdentifier));

            if (propertyName.ToLowerInvariant() != propertyName)
                throw new ArgumentException($"Property name must be a lowercase string, '{propertyName}' given.", nameof(propertyName));

            NamespaceIdentifier = namespaceIdentifier;
            PropertyName = propertyName;
            CollectionIndex = collectionIndex;
        }

        [NotNull]
        public string NamespaceIdentifier { get; }

        [NotNull]
        public string PropertyName { get; }

        public int? CollectionIndex { get; }

        public bool Equals(KvpBagKeyPart other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NamespaceIdentifier == other.NamespaceIdentifier && PropertyName == other.PropertyName && CollectionIndex == other.CollectionIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KvpBagKeyPart) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = NamespaceIdentifier.GetHashCode();
                hashCode = (hashCode * 397) ^ PropertyName.GetHashCode();
                hashCode = (hashCode * 397) ^ CollectionIndex.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(KvpBagKeyPart left, KvpBagKeyPart right) => Equals(left, right);
        public static bool operator !=(KvpBagKeyPart left, KvpBagKeyPart right) => !Equals(left, right);
    }
}