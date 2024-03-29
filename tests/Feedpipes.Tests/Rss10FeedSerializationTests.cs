﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Feedpipes.Rss10;
using Feedpipes.Rss10.Entities;
using Feedpipes.Tests.SampleData;
using Xunit;

namespace Feedpipes.Tests
{
    public class Rss10FeedSerializationTests
    {
        [Theory]
        [ClassData(typeof(ParseAndFormatData))]
        public void ParseAndFormat(SampleFeed embeddedDocument)
        {
            // arrange
            var document1 = embeddedDocument.XDocument;

            // action
            var tryParseResult = Rss10FeedParser.TryParseRss10Feed(document1, out var feed);
            Assert.True(tryParseResult);

            var tryFormatResult = Rss10FeedFormatter.TryFormatRss10Feed(feed, out var document2);
            Assert.True(tryFormatResult);

            var xmlWriterSettings = new XmlWriterSettings { Indent = true };
            var xmlStringBuilder1 = new StringBuilder();
            var xmlStringBuilder2 = new StringBuilder();

            using (var xmlWriter1 = XmlWriter.Create(xmlStringBuilder1, xmlWriterSettings))
            using (var xmlWriter2 = XmlWriter.Create(xmlStringBuilder2, xmlWriterSettings))
            {
                document1.WriteTo(xmlWriter1);
                document2.WriteTo(xmlWriter2);
                xmlWriter1.Flush();
                xmlWriter2.Flush();

                // assert
                var xmlString1 = xmlStringBuilder1.ToString();
                var xmlString2 = xmlStringBuilder2.ToString();
                Assert.Equal(xmlString1, xmlString2);
            }
        }

        public class ParseAndFormatData : SampleFeedTestsClassDataBase
        {
            public override IEnumerable<string> FileNames => new[]
            {
                "_rss10-sample01.xml",
                "_rss10-sample02.xml",
            };
        }

        [Theory]
        [ClassData(typeof(ParseWithoutCrashingData))]
        public void ParseWithoutCrashing(SampleFeed embeddedDocument)
        {
            // arrange
            var document = embeddedDocument.XDocument;

            // action
            // ReSharper disable once UnusedVariable
            var tryParseResult = Rss10FeedParser.TryParseRss10Feed(document, out var parsedFeed);

            // assert
            Assert.True(tryParseResult);
        }

        public class ParseWithoutCrashingData : SampleFeedTestsClassDataBase
        {
            public override bool CustomFilter(SampleFeed x)
            {
                if (x.XDocument?.Root?.Name != Rss10Constants.RdfNamespace + "RDF")
                    return false;

                var recognizedNamespaceNames = Rss10Constants.RecognizedNamespaces.Select(y => y.NamespaceName).ToHashSet();

                if (!recognizedNamespaceNames.Contains(x.XDocument?.Root?.Attribute("xmlns")?.Value))
                    return false;

                return true;
            }
        }

        [Fact]
        public void FormatSampleFeed()
        {
            var feed = new Rss10Feed
            {
                Channel = new Rss10Channel
                {
                    Title = "This is my feed title",
                    Description = "Description of my feed",
                    TextInput = new Rss10TextInput
                    {
                        Name = "TextInput",
                        Title = "My text input",
                        Description = "Description of my text input",
                        Link = "https://example.org/my-text-input",
                        About = "https://example.org/my-text-input",
                    },
                    About = "https://example.org/channel",
                    Link = "https://example.org/channel",
                    Image = new Rss10Image
                    {
                        Title = "My channel image",
                        Link = "https://example.org/channel",
                        Url = "https://example.org/image.png",
                        About = "https://example.org/image.png",
                    },
                    Items = new List<Rss10Item>
                    {
                        new Rss10Item
                        {
                            Title = "My awesome article",
                            Description = "My awesome article description...",
                            Link = "https://example.org/article",
                            About = "https://example.org/article",
                        },
                    },
                },
            };

            var tryFormatResult = Rss10FeedFormatter.TryFormatRss10Feed(feed, out var document);
            Assert.True(tryFormatResult);

            var targetEncoding = Encoding.UTF8;
            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = targetEncoding,
                Indent = true,
            };

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, targetEncoding))
            using (var xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSettings))
            {
                document.WriteTo(xmlWriter);
                xmlWriter.Flush();

                var xmlString = targetEncoding.GetString(memoryStream.ToArray());
                Assert.NotEmpty(xmlString);
            }
        }

        [Fact]
        public void FormatSampleFeedEmpty()
        {
            var feed = new Rss10Feed
            {
                Channel = new Rss10Channel
                {
                    Items = new List<Rss10Item>
                    {
                        new Rss10Item(),
                    },
                },
            };

            var tryFormatResult = Rss10FeedFormatter.TryFormatRss10Feed(feed, out var document);
            Assert.True(tryFormatResult);

            var targetEncoding = Encoding.UTF8;
            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = targetEncoding,
                Indent = true,
            };

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, targetEncoding))
            using (var xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSettings))
            {
                document.WriteTo(xmlWriter);
                xmlWriter.Flush();

                var xmlString = targetEncoding.GetString(memoryStream.ToArray());
                Assert.NotEmpty(xmlString);
            }
        }
    }
}
