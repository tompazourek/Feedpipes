using System;
using System.Collections;
using System.Collections.Generic;
using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.MediaRss;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;
using Feedpipes.Syndication.Extensions.RssAtom10;
using Feedpipes.Syndication.Extensions.WellFormedWeb;
using JetBrains.Annotations;

namespace Feedpipes.Syndication.Extensions
{
    public class ExtensionManifestDirectory : ICollection<ExtensionManifest>
    {
        #region Defaults

        private static readonly IList<ExtensionManifest> DefaultXmlBased = new List<ExtensionManifest>
        {
            new CreativeCommonsExtensionManifest(),
            new DublinCoreExtensionManifest(),
            new MediaRssExtensionManifest(),
            new Rss10ContentExtensionManifest(),
            new Rss10SlashExtensionManifest(),
            new Rss10SyndicationExtensionManifest(),
            new WellFormedWebExtensionManifest(),
        };

        public static ExtensionManifestDirectory DefaultForRss { get; set; } = new ExtensionManifestDirectory(DefaultXmlBased)
        {
            new RssAtom10ExtensionManifest(),
        };

        public static ExtensionManifestDirectory DefaultForAtom { get; set; } = new ExtensionManifestDirectory(DefaultXmlBased);

        public static ExtensionManifestDirectory DefaultForJsonFeed { get; set; } = new ExtensionManifestDirectory();

        #endregion

        #region Public

        public ExtensionManifestDirectory()
        {
        }

        public ExtensionManifestDirectory([NotNull] IEnumerable<ExtensionManifest> extensionManifests)
        {
            if (extensionManifests == null)
                throw new ArgumentNullException(nameof(extensionManifests));

            foreach (var extensionManifest in extensionManifests)
            {
                Add(extensionManifest);
            }
        }

        public bool TryGetExtensionManifestByExtensionType(Type extensionType, out ExtensionManifest extensionManifest)
            => _extensionManifestsByType.TryGetValue(extensionType, out extensionManifest);

        #endregion

        #region Private

        private readonly IDictionary<Type, ExtensionManifest> _extensionManifestsByType = new Dictionary<Type, ExtensionManifest>();
        private readonly ICollection<ExtensionManifest> _innerCollection = new List<ExtensionManifest>();

        #endregion

        #region ICollection

        public IEnumerator<ExtensionManifest> GetEnumerator() => _innerCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _innerCollection).GetEnumerator();

        public void Add(ExtensionManifest item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_extensionManifestsByType.ContainsKey(item.ExtensionType))
                throw new ArgumentException($"Cannot add extension for type '{item.ExtensionType}' as it's already present in the directory.");

            _innerCollection.Add(item);
            _extensionManifestsByType[item.ExtensionType] = item;
        }

        public void Clear()
        {
            _innerCollection.Clear();
            _extensionManifestsByType.Clear();
        }

        public bool Contains(ExtensionManifest item) => _innerCollection.Contains(item);

        public void CopyTo(ExtensionManifest[] array, int arrayIndex)
        {
            _innerCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(ExtensionManifest item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = _innerCollection.Remove(item);
            if (result)
            {
                _extensionManifestsByType.Remove(item.ExtensionType);
            }

            return result;
        }

        public int Count => _innerCollection.Count;
        public bool IsReadOnly => _innerCollection.IsReadOnly;

        #endregion
    }
}