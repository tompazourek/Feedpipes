using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Feedpipes.Syndication.Atom10;
using Feedpipes.Syndication.Atom10.Entities;
using Feedpipes.Syndication.SampleData;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class Atom10FeedSerializationTests
    {
        [Theory]
        [ClassData(typeof(ParseAndFormatData))]
        public void ParseAndFormat(SampleFeed embeddedDocument)
        {
            // arrange
            var document1 = embeddedDocument.XDocument;

            // action
            var tryParseResult = Atom10FeedParser.TryParseAtom10Feed(document1, out var feed);
            Assert.True(tryParseResult);

            var tryFormatResult = Atom10FeedFormatter.TryFormatAtom10Feed(feed, out var document2);
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
                "_atom10-sample01.xml",
                "_atom10-sample02.xml",
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
            var tryParseResult = Atom10FeedParser.TryParseAtom10Feed(document, out var parsedFeed);

            // assert
            Assert.True(tryParseResult);
        }

        public class ParseWithoutCrashingData : SampleFeedTestsClassDataBase
        {
            public override bool CustomFilter(SampleFeed x) => x.XDocument?.Root?.Name.LocalName == "feed";
        }

        [Fact]
        public void FormatSampleFeed()
        {
            var feed = new Atom10Feed
            {
                Generator = new Atom10Generator
                {
                    Value = "My Awesome Generator 3000",
                    Version = "3000",
                    Uri = "https://example.org/generator",
                },
                Title = new Atom10Text { Value = "This is my <strong>feed</strong> title", Type = "html" },
                Categories = new List<Atom10Category>
                {
                    new Atom10Category { Term = "alpha", Label = "Alpha", Scheme = "https://example.org/categories" },
                    new Atom10Category { Term = "beta", Label = "Beta", Scheme = "https://example.org/categories" },
                    new Atom10Category { Term = "gamma", Label = "Gamma", Scheme = "https://example.org/categories" },
                },
                Id = "https://example.org/feed",
                Updated = new DateTimeOffset(2019, 01, 01, 4, 30, 00, TimeSpan.FromHours(0)),
                Subtitle = new Atom10Text { Value = "This is my feed's subtitle" },
                Rights = new Atom10Text { Value = "All rights reserved." },
                Contributors = new List<Atom10Person>
                {
                    new Atom10Person
                    {
                        Name = "John",
                        Uri = "https://example.org/john",
                        Email = "john@example.org",
                    },
                },
                Authors = new List<Atom10Person>
                {
                    new Atom10Person
                    {
                        Name = "Jane",
                        Uri = "https://example.org/jane",
                        Email = "jane@example.org",
                    },
                    new Atom10Person
                    {
                        Name = "John",
                        Uri = "https://example.org/john",
                        Email = "john@example.org",
                    },
                },
                Lang = "en-US",
                Icon = "https://example.org/icon.png",
                Logo = "https://example.org/logo.png",
                Links = new List<Atom10Link>
                {
                    new Atom10Link
                    {
                        Title = "My link",
                        Href = "https://example.org/my-link",
                        Type = "text/html",
                        Length = 1000,
                        Hreflang = "en-GB",
                        Rel = "via",
                    },
                    new Atom10Link
                    {
                        Href = "https://example.org/feed-alternate",
                    },
                    new Atom10Link
                    {
                        Href = "https://example.org/feed",
                        Rel = "self",
                    },
                },
                Entries = new List<Atom10Entry>
                {
                    new Atom10Entry
                    {
                        Title = new Atom10Text { Value = "My awesome article" },
                        Categories = new List<Atom10Category> { new Atom10Category { Term = "delta", Label = "Delta", Scheme = "https://example.org/categories" } },
                        Published = new DateTimeOffset(2019, 01, 01, 5, 30, 00, TimeSpan.FromHours(0)),
                        Updated = new DateTimeOffset(2019, 01, 05, 5, 30, 00, TimeSpan.FromHours(-5)),
                        Id = "https://example.org/article-permalink",
                        Authors = new List<Atom10Person>
                        {
                            new Atom10Person
                            {
                                Name = "Tom",
                                Uri = "https://example.org/tom",
                                Email = "tom@example.org",
                            },
                        },
                        Contributors = new List<Atom10Person>
                        {
                            new Atom10Person
                            {
                                Name = "Sarah",
                                Uri = "https://example.org/sarah",
                                Email = "sarah@example.org",
                            },
                        },
                        Content = new Atom10Content
                        {
                            Value = "My <strong>HTML</strong> content.",
                            Type = "html",
                        },
                        Summary = new Atom10Text { Value = "<div xmlns=\"http://www.w3.org/1999/xhtml\">My awesome article summary...</div>", Type = "xhtml" },
                        Rights = new Atom10Text { Value = "All rights reserved again." },
                        Links = new List<Atom10Link>
                        {
                            new Atom10Link
                            {
                                Href = "https://example.org/article-alternate?hl=cs",
                                Hreflang = "cs-CZ",
                            },
                        },
                        Source = new Atom10Source
                        {
                            Id = "https://example.org/source",
                            Title = new Atom10Text { Value = "Example, Inc." },
                            Updated = new DateTimeOffset(2003, 12, 13, 18, 30, 2, TimeSpan.Zero),
                        },
                    }
                },
            };

            var tryFormatResult = Atom10FeedFormatter.TryFormatAtom10Feed(feed, out var document);
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
            var feed = new Atom10Feed
            {
                Entries = new List<Atom10Entry>
                {
                    new Atom10Entry()
                },
            };

            var tryFormatResult = Atom10FeedFormatter.TryFormatAtom10Feed(feed, out var document);
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