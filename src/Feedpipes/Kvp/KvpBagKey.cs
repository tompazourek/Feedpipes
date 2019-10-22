using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Feedpipes.Kvp
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class KvpBagKey : IEquatable<KvpBagKey>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => KvpBagStringPairFormatter.TryFormatKvpKey(this, out var result) ? result : null;

        public override string ToString() => DebuggerDisplay;

        public KvpBagKey([ItemNotNull] IList<KvpBagKeyPart> parts)
        {
            Parts = parts?.ToList() ?? throw new ArgumentNullException(nameof(parts));

            if (Parts.Count == 0)
                throw new ArgumentNullException(nameof(parts), "Cannot pass empty list of key parts.");
        }

        public KvpBagKey([ItemNotNull] params KvpBagKeyPart[] parts) : this(parts?.ToList())
        {
        }

        public KvpBagKey([ItemNotNull] IEnumerable<KvpBagKeyPart> parts) : this(parts?.ToList())
        {
        }

        [NotNull, ItemNotNull]
        public IReadOnlyList<KvpBagKeyPart> Parts { get; }

        public bool Equals(KvpBagKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Parts.SequenceEqual(other.Parts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KvpBagKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 19;
                foreach (var part in Parts)
                {
                    hash = hash * 31 + part.GetHashCode();
                }

                return hash;
            }
        }

        public static bool operator ==(KvpBagKey left, KvpBagKey right) => Equals(left, right);

        public static bool operator !=(KvpBagKey left, KvpBagKey right) => !Equals(left, right);
    }
}