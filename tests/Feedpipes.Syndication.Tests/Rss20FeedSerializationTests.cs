using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Feedpipes.Syndication.Rss20;
using Feedpipes.Syndication.Rss20.Entities;
using Feedpipes.Syndication.SampleData;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class Rss20FeedSerializationTests
    {
        [Theory]
        [ClassData(typeof(ParseAndFormatData))]
        public void ParseAndFormat(SampleFeed embeddedDocument)
        {
            // arrange
            var document1 = embeddedDocument.Document;

            // action
            var tryParseResult = Rss20FeedParser.TryParseRss20Feed(document1, out var feed);
            Assert.True(tryParseResult);

            var tryFormatResult = Rss20FeedFormatter.TryFormatRss20Feed(feed, out var document2);
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
            public override IEnumerable<string> XmlFileNames => new[]
            {
                "_rss20-sample01",
                "_rss20-sample02",
            };
        }

        [Theory]
        [ClassData(typeof(ParseWithoutCrashingData))]
        public void ParseWithoutCrashing(SampleFeed embeddedDocument)
        {
            // arrange
            var document = embeddedDocument.Document;

            // action
            // ReSharper disable once UnusedVariable
            var tryParseResult = Rss20FeedParser.TryParseRss20Feed(document, out var parsedFeed);

            // assert
            Assert.True(tryParseResult);
        }
        
        public class ParseWithoutCrashingData : SampleFeedTestsClassDataBase
        {
            public override bool CustomFilter(SampleFeed x)
            {
                // skip feeds without <rss> root
                return x.Document?.Root?.Name == "rss";
            }
        }

        [Fact]
        public void FormatSampleFeed()
        {
            var feed = new Rss20Feed
            {
                Channel = new Rss20Channel
                {
                    Generator = "My Awesome Generator 3000",
                    Title = "This is my feed title",
                    Categories = new List<Rss20Category> { new Rss20Category { Name = "Alpha" }, new Rss20Category { Name = "Beta" }, new Rss20Category { Name = "Gama", Domain = "https://example.org/category-domain" }, },
                    Copyright = "Copyright 2019, All rights reserved",
                    WebMaster = "webmaster@example.org (John Doe)",
                    ManagingEditor = "managing-editor@example.org (Jane Doe)",
                    Language = "en-US",
                    PubDate = new DateTimeOffset(2019, 01, 01, 12, 30, 00, TimeSpan.FromHours(2)),
                    Cloud = new Rss20Cloud
                    {
                        Domain = "https://example.org/cloud",
                        RegisterProcedure = "abc",
                        Protocol = "def",
                        Path = "/ghi",
                        Port = "9000",
                    },
                    Description = "Description of my feed",
                    LastBuildDate = new DateTimeOffset(2019, 01, 01, 4, 30, 00, TimeSpan.FromHours(0)),
                    Ttl = TimeSpan.FromHours(3),
                    TextInput = new Rss20TextInput
                    {
                        Name = "TextInput", Title = "My text input", Description = "Description of my text input", Link = "https://example.org/my-text-input",
                    },
                    SkipHours = new List<int> { 1, 2, 3 },
                    Docs = "https://example.org/docs",
                    SkipDays = new List<DayOfWeek> { DayOfWeek.Friday, DayOfWeek.Monday, },
                    Link = "https://example.org/channel",
                    Image = new Rss20Image
                    {
                        Title = "My channel image",
                        Width = 100,
                        Height = 50,
                        Description = "My channel image description",
                        Link = "https://example.org/channel",
                        Url = "https://example.org/image.png",
                    },
                    Items = new List<Rss20Item>
                    {
                        new Rss20Item
                        {
                            Title = "My awesome article",
                            Categories = new List<Rss20Category> { new Rss20Category { Name = "Delta", Domain = "https://example.org/category-domain" }, },
                            PubDate = new DateTimeOffset(2019, 01, 01, 5, 30, 00, TimeSpan.FromHours(0)),
                            Description = "My awesome article description...",
                            Link = "https://example.org/article",
                            Author = "author@example.org (John Doe)",
                            Comments = "https://example.org/comments",
                            Guid = new Rss20Guid { Value = "https://example.org/article-permalink", IsPermaLink = true, },
                            Source = new Rss20Source { Name = "Article source X", Url = "https://example.org/article-source-x", },
                            Enclosures = new List<Rss20Enclosure> { new Rss20Enclosure { Url = "https://example.org/enclosure-video.mp4", Length = "3000", Type = "video/mp4", }, },
                        },
                    },
                },
            };

            var tryFormatResult = Rss20FeedFormatter.TryFormatRss20Feed(feed, out var document);
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