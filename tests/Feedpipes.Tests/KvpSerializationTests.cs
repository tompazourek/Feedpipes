using System.Collections.Generic;
using Feedpipes.Syndication.Kvp;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class KvpSerializationTests
    {
        [Fact]
        public void Format_Simple()
        {
            var kvpBag = new KvpBag
            {
                { new KvpBagKey(new KvpBagKeyPart("fp", "title"), new KvpBagKeyPart("fp", "text")), "My text" },
                { new KvpBagKey(new KvpBagKeyPart("fp", "images", 0), new KvpBagKeyPart("fp", "url")), "https://example.org/image.png" },
                { new KvpBagKey(new KvpBagKeyPart("fp", "images", 0), new KvpBagKeyPart("dc", "creator")), "John Doe" },
            };
            var formatResult = KvpBagStringPairFormatter.TryFormatKvpBag(kvpBag, out var stringPairs);
            Assert.True(formatResult);

            Assert.Equal(kvpBag.Count, stringPairs.Count);
            Assert.Equal(stringPairs["fp:title.text"], "My text");
            Assert.Equal(stringPairs["fp:images[0].url"], "https://example.org/image.png");
            Assert.Equal(stringPairs["fp:images[0].dc:creator"], "John Doe");
        }

        [Fact]
        public void Parse_Simple()
        {
            var stringPairs = new Dictionary<string, string>
            {
                ["fp:title.text"] = "My text",
                ["fp:images[0].url"] = "https://example.org/image.png", 
                ["fp:images[0].dc:creator"] = "John Doe",
            };

            var parseResult = KvpBagStringPairParser.TryParseKvpBag(stringPairs, out var kvpBag);
            Assert.True(parseResult);
            
            Assert.Equal(kvpBag.Count, stringPairs.Count);
            Assert.Equal(kvpBag[new KvpBagKey(new KvpBagKeyPart("fp", "title"), new KvpBagKeyPart("fp", "text"))], "My text");
            Assert.Equal(kvpBag[new KvpBagKey(new KvpBagKeyPart("fp", "images", 0), new KvpBagKeyPart("fp", "url"))], "https://example.org/image.png");
            Assert.Equal(kvpBag[new KvpBagKey(new KvpBagKeyPart("fp", "images", 0), new KvpBagKeyPart("dc", "creator"))], "John Doe");
        }
    }
}