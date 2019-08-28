using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Feedpipes.Syndication.JsonFeedFormat;
using Feedpipes.Syndication.JsonFeedFormat.Entities;
using Feedpipes.Syndication.SampleData;
using Newtonsoft.Json;
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

        [Theory]
        [ClassData(typeof(ParseAndFormatData))]
        public void ParseAndFormat(SampleFeed embeddedDocument)
        {
            // arrange
            var document1 = embeddedDocument.JsonDocument;

            // action
            var tryParseResult = JsonFeedParser.TryParseJsonFeed(document1, out var feed);
            Assert.True(tryParseResult);

            var tryFormatResult = JsonFeedFormatter.TryFormatJsonFeed(feed, out var document2);
            Assert.True(tryFormatResult);

            using (var stringWriter1 = new StringWriter())
            using (var stringWriter2 = new StringWriter())
            using (var jsonWriter1 = new JsonTextWriter(stringWriter1)
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Indentation = 4,
            })
            using (var jsonWriter2 = new JsonTextWriter(stringWriter2)
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Indentation = 4,
            })
            {
                document1.WriteTo(jsonWriter1);
                document2.WriteTo(jsonWriter2);
                jsonWriter1.Flush();
                jsonWriter2.Flush();

                // assert
                var jsonString1 = stringWriter1.ToString();
                var jsonString2 = stringWriter2.ToString();
                Assert.Equal(jsonString1, jsonString2);
            }
        }

        public class ParseAndFormatData : SampleFeedTestsClassDataBase
        {
            public override IEnumerable<string> FileNames => new[]
            {
                "_jsonfeed1-sample05.json",
            };
        }

        [Fact]
        public void FormatSampleFeed()
        {
            var feed = new JsonFeed
            {
                FeedUrl = "https://example.org/feed.json",
                Title = "This is my feed title",
                HomePageUrl = "https://example.org/homepage",
                Author = new JsonFeedAuthor
                {
                    Name = "John Doe",
                    Avatar = "https://example.org/john-doe/avatar.png",
                    Url = "mailto:john.doe@example.org",
                },
                Description = "This is my feed description",
                Expired = false,
                Favicon = "https://example.org/favicon.ico",
                Icon = "https://example.org/icon.png",
                NextUrl = "https://example.org/feed.json?offset=1",
                UserComment = "This is a user comment",
                Hubs = new List<JsonFeedHub>
                {
                    new JsonFeedHub
                    {
                        Type = "rssCloud",
                        Url = "https://example.org/rss-cloud-hub",
                    },
                    new JsonFeedHub
                    {
                        Type = "WebSub",
                        Url = "https://example.org/web-sub-hub",
                    },
                },
                Items = new List<JsonFeedItem>
                {
                    new JsonFeedItem
                    {
                        Id = "1",
                        Title = "My awesome article",
                        DatePublished = new DateTimeOffset(2019, 01, 01, 5, 30, 00, TimeSpan.FromHours(0)),
                        Url = "https://example.org/article",
                        ExternalUrl = "https://example.org/article-external",
                        Image = "https://example.org/article-image.png",
                        DateModified = new DateTimeOffset(2019, 01, 02, 5, 30, 00, TimeSpan.FromHours(0)),
                        ContentText = "This is a text content",
                        ContentHtml = "This is a <strong>HTML</strong> content",
                        BannerImage = "https://example.org/article-banner-image.png",
                        Summary = "This is a summary of the article",
                        Author = new JsonFeedAuthor
                        {
                            Name = "Jane Doe",
                            Avatar = "https://example.org/jane-doe/avatar.png",
                            Url = "mailto:jane.doe@example.org",
                        },
                        Tags = new List<string> { "alpha", "beta", "gama" },
                        Attachments = new List<JsonFeedAttachment>
                        {
                            new JsonFeedAttachment
                            {
                                MimeType = "video/mp4",
                                Title = "Video attachment",
                                Url = "https://example.org/video.mp4",
                                SizeInBytes = 3000,
                                DurationInSeconds = 16,
                            },
                        },
                    },
                },
            };

            var tryFormatResult = JsonFeedFormatter.TryFormatJsonFeed(feed, out var document);
            Assert.True(tryFormatResult);

            var targetEncoding = Encoding.UTF8;
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, targetEncoding))
            using (var jsonWriter = new JsonTextWriter(streamWriter)
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Indentation = 4,
            })
            {
                document.WriteTo(jsonWriter);
                jsonWriter.Flush();

                var jsonString = targetEncoding.GetString(memoryStream.ToArray());
                Assert.NotEmpty(jsonString);
            }
        }

        [Fact]
        public void FormatSampleFeedEmpty()
        {
            var feed = new JsonFeed
            {
                Items = new List<JsonFeedItem>
                {
                    new JsonFeedItem(),
                },
            };

            var tryFormatResult = JsonFeedFormatter.TryFormatJsonFeed(feed, out var document);
            Assert.True(tryFormatResult);

            var targetEncoding = Encoding.UTF8;
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, targetEncoding))
            using (var jsonWriter = new JsonTextWriter(streamWriter)
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Indentation = 4,
            })
            {
                document.WriteTo(jsonWriter);
                jsonWriter.Flush();

                var jsonString = targetEncoding.GetString(memoryStream.ToArray());
                Assert.NotEmpty(jsonString);
            }
        }
    }
}