using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Feedpipes.Syndication.SampleData
{
    public abstract class SampleFeedTestsClassDataBase : IEnumerable<object[]>
    {
        public abstract IEnumerable<string> XmlFileNames { get; }

        public IEnumerator<object[]> GetEnumerator()
        {
            var xmlFileNamesSet = XmlFileNames.ToHashSet();
            return SampleFeedDirectory
                .GetSampleFeeds()
                .Where(x => xmlFileNamesSet.Contains(x.FileName))
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}