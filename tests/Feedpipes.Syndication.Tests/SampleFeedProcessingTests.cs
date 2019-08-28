using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Feedpipes.Syndication.Rss20;
using Feedpipes.Syndication.SampleData;
using Feedpipes.Syndication.Utils.Xml;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class SampleFeedProcessingTests
    {
        [Fact(Skip = "Only helper to download tests, fill in the target path before use.")]
        public async Task DebugDownloadSampleFeedXml()
        {
            var sampleFeeds = SampleFeedDirectory.GetSampleFeeds();
            foreach (var sampleFeed in sampleFeeds)
            {
                var file = @"C:\SOLUTION_PATH\tests\Feedpipes.Syndication.SampleData\Files\" + sampleFeed.FileName;
                if (File.Exists(file))
                    continue;

                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(sampleFeed.FeedUrl);
                        response.EnsureSuccessStatusCode();

                        using (var fileStream = File.OpenWrite(file))
                        {
                            await response.Content.CopyToAsync(fileStream);
                        }
                    }
                }
                catch (Exception)
                {
                    Debugger.Break();
                }
            }
        }

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