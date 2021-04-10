using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Feedpipes.Utils.Xml
{
    /// <summary>
    /// Helper for maintaining namespace aliases.
    /// </summary>
    public class XNamespaceAliasSet : ISet<XAttribute>
    {
        #region Internals

        private readonly ISet<XAttribute> _internalSet = new HashSet<XAttribute>(new XAttributeComparer());

        #endregion

        #region Public methods

        public void EnsureNamespaceAlias(string alias, XNamespace ns)
        {
            _internalSet.Add(string.IsNullOrEmpty(alias)
                ? new XAttribute("xmlns", ns.NamespaceName)
                : new XAttribute(XNamespace.Xmlns + alias, ns.NamespaceName));
        }

        #endregion

        #region Equality comparer

        private class XAttributeComparer : IEqualityComparer<XAttribute>
        {
            public bool Equals(XAttribute x, XAttribute y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;

                return Equals(x.Name, y.Name) && string.Equals(x.Value, y.Value);
            }

            public int GetHashCode(XAttribute obj)
            {
                unchecked
                {
                    return (obj.Name.GetHashCode() * 397) ^ obj.Value.GetHashCode();
                }
            }
        }

        #endregion

        #region ISet members

        public IEnumerator<XAttribute> GetEnumerator() => _internalSet.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_internalSet).GetEnumerator();

        void ICollection<XAttribute>.Add(XAttribute item) => _internalSet.Add(item);

        public void ExceptWith(IEnumerable<XAttribute> other) => _internalSet.ExceptWith(other);

        public void IntersectWith(IEnumerable<XAttribute> other) => _internalSet.IntersectWith(other);

        public bool IsProperSubsetOf(IEnumerable<XAttribute> other) => _internalSet.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<XAttribute> other) => _internalSet.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<XAttribute> other) => _internalSet.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<XAttribute> other) => _internalSet.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<XAttribute> other) => _internalSet.Overlaps(other);

        public bool SetEquals(IEnumerable<XAttribute> other) => _internalSet.SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<XAttribute> other) => _internalSet.SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<XAttribute> other) => _internalSet.UnionWith(other);

        bool ISet<XAttribute>.Add(XAttribute item) => _internalSet.Add(item);

        public void Clear() => _internalSet.Clear();

        public bool Contains(XAttribute item) => _internalSet.Contains(item);

        public void CopyTo(XAttribute[] array, int arrayIndex) => _internalSet.CopyTo(array, arrayIndex);

        public bool Remove(XAttribute item) => _internalSet.Remove(item);

        public int Count => _internalSet.Count;

        public bool IsReadOnly => _internalSet.IsReadOnly;

        #endregion
    }
}
