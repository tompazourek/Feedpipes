using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Kvp
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class KvpBag : IDictionary<KvpBagKey, KvpBagValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Count);

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly IDictionary<KvpBagKey, KvpBagValue> _innerDictionary = new ConcurrentDictionary<KvpBagKey, KvpBagValue>();

        #region Delegated interface

        public IEnumerator<KeyValuePair<KvpBagKey, KvpBagValue>> GetEnumerator() => _innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _innerDictionary).GetEnumerator();

        public void Add(KeyValuePair<KvpBagKey, KvpBagValue> item)
        {
            if (item.Value == null)
                throw new ArgumentNullException(nameof(item), "Item value cannot be null or empty.");

            _innerDictionary.Add(item);
        }

        public void Clear() => _innerDictionary.Clear();

        public bool Contains(KeyValuePair<KvpBagKey, KvpBagValue> item) => _innerDictionary.Contains(item);

        public void CopyTo(KeyValuePair<KvpBagKey, KvpBagValue>[] array, int arrayIndex) => _innerDictionary.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<KvpBagKey, KvpBagValue> item) => _innerDictionary.Remove(item);

        public int Count => _innerDictionary.Count;

        public bool IsReadOnly => _innerDictionary.IsReadOnly;

        public void Add(KvpBagKey key, KvpBagValue value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Item value cannot be null or empty.");

            _innerDictionary.Add(key, value);
        }

        public bool ContainsKey(KvpBagKey key) => _innerDictionary.ContainsKey(key);

        public bool Remove(KvpBagKey key) => _innerDictionary.Remove(key);

        public bool TryGetValue(KvpBagKey key, out KvpBagValue value) => _innerDictionary.TryGetValue(key, out value);

        public KvpBagValue this[KvpBagKey key]
        {
            get => _innerDictionary[key];
            set => _innerDictionary[key] = value ?? throw new ArgumentNullException(nameof(value), "Item value cannot be null or empty.");
        }

        public ICollection<KvpBagKey> Keys => _innerDictionary.Keys;
        public ICollection<KvpBagValue> Values => _innerDictionary.Values;

        #endregion
    }
}