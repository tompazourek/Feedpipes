using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Feedpipes.Tests.SampleData
{
    public abstract class SampleFeedTestsClassDataBase : IEnumerable<object[]>
    {
        public virtual IEnumerable<string> FileNames { get; } = null;

        public IEnumerator<object[]> GetEnumerator()
        {
            var fileNamesSet = FileNames?.ToHashSet();
            return SampleFeedDirectory
                .GetSampleFeeds()
                .Where(x => fileNamesSet?.Contains(x.FileName) != false)
                .Where(CustomFilter)
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public virtual bool CustomFilter(SampleFeed x) => true;
    }
}
