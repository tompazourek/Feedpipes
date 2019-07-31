using System.Diagnostics;
using System.Linq;
using Feedpipes.Syndication.Rss20;
using Feedpipes.Syndication.SampleData;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class SampleFeedProcessingTests
    {
        [Fact]
        public void DebugRssPossibleNamespaceDeclarations()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();
            var namespaceSet = new XNamespaceAliasSet();

            foreach (var feed in sampleFeeds)
            {
                var documentRoot = feed.Document.Root;

                if (documentRoot?.Name != "rss")
                    continue;

                var namespaceDeclarations = documentRoot
                    .Attributes()
                    .Where(x => x.IsNamespaceDeclaration)
                    .ToList();

                foreach (var namespaceDeclaration in namespaceDeclarations)
                {
                    namespaceSet.EnsureNamespaceAlias(namespaceDeclaration.Name.LocalName, namespaceDeclaration.Value);
                }
            }

            Debugger.Break(); // take a look at "namespaceSet"
        }

        [Fact]
        public void DebugRootNodes()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();

            var feedsByRoot = sampleFeeds
                .GroupBy(feed => feed.Document.Root?.Name.LocalName)
                .ToDictionary(x => x.Key, x => x.ToList());

            Debugger.Break(); // take a look at "feedsByRoot"
        }
        
        [Fact]
        public void DebugRssGenerators()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();

            var feedsByGenerator = sampleFeeds
                .Select(x =>
                {
                    var tryParseResult = Rss20FeedParser.TryParseRss20Feed(x.Document, out var rss20Feed);
                    return (feed: x, rss20Feed: rss20Feed, tryParseResult: tryParseResult);
                })
                .Where(x => x.tryParseResult)
                .Select(x => (feed: x.feed, generator: x.rss20Feed.Channel?.Generator))
                .GroupBy(x => x.generator ?? string.Empty, x => x.feed)
                .ToDictionary(x => x.Key, x => x.ToList());

            Debugger.Break(); // take a look at "feedsByGenerator"
        }
    }
}