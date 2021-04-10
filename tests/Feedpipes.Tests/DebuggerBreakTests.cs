using System.Diagnostics;
using System.Linq;
using Feedpipes.Rss20;
using Feedpipes.Tests.SampleData;
using Feedpipes.Utils.Xml;
using Xunit;

namespace Feedpipes.Tests
{
    public class DebuggerBreakTests
    {
        [Fact]
        public void DebugInvalidXml()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();

            var feed = sampleFeeds
                .First(x => x.FileName == "feeds-feedburner-com-seomoz.xml");

            // ReSharper disable once UnusedVariable
            var doc = feed.XDocument;
            Debugger.Break();
        }

        [Fact]
        public void DebugPossibleNamespaceDeclarations()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();
            var namespaceSet = new XNamespaceAliasSet();

            foreach (var feed in sampleFeeds)
            {
                var documentRoot = feed.XDocument?.Root;

                if (documentRoot == null)
                    continue;

                var namespaceDeclarations = documentRoot
                    .Attributes()
                    .Where(x => x.IsNamespaceDeclaration)
                    .ToList();

                foreach (var namespaceDeclaration in namespaceDeclarations)
                {
                    var alias = namespaceDeclaration.Name.LocalName;
                    if (alias == "xmlns")
                    {
                        alias = null;
                    }

                    namespaceSet.EnsureNamespaceAlias(alias, namespaceDeclaration.Value);
                }
            }

            Debugger.Break(); // take a look at "namespaceSet"
        }

        [Fact]
        public void DebugRootNodes()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();

            // ReSharper disable once UnusedVariable
            var feedsByRoot = sampleFeeds
                .Where(x => x.XDocument != null)
                .GroupBy(feed => feed.XDocument.Root?.Name.LocalName)
                .ToDictionary(x => x.Key, x => x.ToList());

            Debugger.Break(); // take a look at "feedsByRoot"
        }

        [Fact]
        public void DebugRssGenerators()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();

            // ReSharper disable once UnusedVariable
            var feedsByGenerator = sampleFeeds
                .Where(x => x.XDocument != null)
                .Select(x =>
                {
                    var tryParseResult = Rss20FeedParser.TryParseRss20Feed(x.XDocument, out var rss20Feed);
                    return (feed: x, rss20Feed, tryParseResult);
                })
                .Where(x => x.tryParseResult)
                .Select(x => (x.feed, generator: x.rss20Feed.Channel?.Generator))
                .GroupBy(x => x.generator ?? string.Empty, x => x.feed)
                .ToDictionary(x => x.Key, x => x.ToList());

            Debugger.Break(); // take a look at "feedsByGenerator"
        }
    }
}
