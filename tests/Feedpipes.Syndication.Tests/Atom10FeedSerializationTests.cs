using Feedpipes.Syndication.Atom10;
using Feedpipes.Syndication.SampleData;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class Atom10FeedSerializationTests
    {
        [Theory]
        [ClassData(typeof(ParseWithoutCrashingData))]
        public void ParseWithoutCrashing(SampleFeed embeddedDocument)
        {
            // arrange
            var document = embeddedDocument.Document;

            // action
            // ReSharper disable once UnusedVariable
            var tryParseResult = Atom10FeedParser.TryParseAtom10Feed(document, out var parsedFeed);

            // assert
            Assert.True(tryParseResult);
        }
        
        public class ParseWithoutCrashingData : SampleFeedTestsClassDataBase
        {
            public override bool CustomFilter(SampleFeed x)
            {
                // skip feeds without <feed> root
                return x.Document?.Root?.Name.LocalName == "feed";
            }
        }
    }
}