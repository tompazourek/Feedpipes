using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Feedpipes.Syndication.SampleData
{
    public abstract class SampleFeedTestsClassDataBase : IEnumerable<object[]>
    {
        public virtual IEnumerable<string> XmlFileNames { get; } = null;

        public IEnumerator<object[]> GetEnumerator()
        {
            var xmlFileNamesSet = XmlFileNames?.ToHashSet();
            return SampleFeedDirectory
                .GetSampleFeeds()
                .Where(x => xmlFileNamesSet?.Contains(x.FileName) != false)
                .Where(CustomFilter)
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public virtual bool CustomFilter(SampleFeed x) => true;
    }
}