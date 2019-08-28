using Feedpipes.Syndication.JsonFeed;
using Feedpipes.Syndication.SampleData;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class JsonFeedSerializationTests
    {
        [Theory]
        [ClassData(typeof(ParseWithoutCrashingData))]
        public void ParseWithoutCrashing(SampleFeed embeddedDocument)
        {
            // arrange
            var document = embeddedDocument.JsonDocument;

            // action
            // ReSharper disable once UnusedVariable
            var tryParseResult = JsonFeedParser.TryParseJsonFeed(document, out var parsedFeed);

            // assert
            Assert.True(tryParseResult);
        }

        public class ParseWithoutCrashingData : SampleFeedTestsClassDataBase
        {
            public override bool CustomFilter(SampleFeed x) => x.JsonDocument != default;
        }
    }
}