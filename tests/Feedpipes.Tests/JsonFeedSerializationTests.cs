using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Feedpipes.Extensions;
using Feedpipes.JsonFeedFormat;
using Feedpipes.JsonFeedFormat.Entities;
using Feedpipes.Tests.SampleData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

// ReSharper disable UseObjectOrCollectionInitializer

namespace Feedpipes.Tests
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

        public class BlueShedSampleExtensionManifest : ExtensionManifest<BlueShedSampleExtension>
        {
            protected override bool TryParseJObjectExtension(JObject parentObject, ExtensionManifestDirectory extensionManifestDirectory, out BlueShedSampleExtension extension)
            {
                extension = default;

                var property = parentObject.Property("_blue_shed");
                if (property == null)
                    return false;

                if (property.Value?.Type != JTokenType.Object)
                    return false;

                return TryParseBlueShedSampleExtensionObject((JObject)property.Value, out extension);
            }

            protected override bool TryFormatJObjectExtension(BlueShedSampleExtension extensionToFormat, ExtensionManifestDirectory extensionManifestDirectory, out IList<JToken> tokens)
            {
                tokens = default;

                if (!TryFormatBlueShedSampleExtensionObject(extensionToFormat, out var extensionObject))
                    return false;

                tokens = new JToken[] { new JProperty("_blue_shed", extensionObject) };
                return true;
            }


            private static bool TryParseBlueShedSampleExtensionObject(JObject extensionObject, out BlueShedSampleExtension parsedExtension)
            {
                parsedExtension = default;

                if (extensionObject == null)
                    return false;

                parsedExtension = new BlueShedSampleExtension();
                parsedExtension.About = extensionObject.Property("about")?.Value?.Value<string>();
                parsedExtension.Explicit = extensionObject.Property("explicit")?.Value?.Value<bool>();
                parsedExtension.Copyright = extensionObject.Property("copyright")?.Value?.Value<string>();
                parsedExtension.Owner = extensionObject.Property("owner")?.Value?.Value<string>();
                parsedExtension.Subtitle = extensionObject.Property("subtitle")?.Value?.Value<string>();

                return true;
            }

            private static bool TryFormatBlueShedSampleExtensionObject(BlueShedSampleExtension extensionToFormat, out JObject extensionObject)
            {
                extensionObject = default;

                if (extensionToFormat == null)
                    return false;

                extensionObject = new JObject();

                extensionObject.Add("about", extensionToFormat.About);

                if (extensionToFormat.Explicit != null)
                {
                    extensionObject.Add("explicit", extensionToFormat.Explicit.Value);
                }

                extensionObject.Add("copyright", extensionToFormat.Copyright);
                extensionObject.Add("owner", extensionToFormat.Owner);
                extensionObject.Add("subtitle", extensionToFormat.Subtitle);

                return true;
            }
        }

        /// <summary>
        /// Sample JSON Feed extension.
        /// See "_blue_shed" in JSON Feed Spec (https://jsonfeed.org/version/1)
        /// </summary>
        public class BlueShedSampleExtension : IExtensionEntity
        {
            public string About { get; set; }
            public bool? Explicit { get; set; }
            public string Copyright { get; set; }
            public string Owner { get; set; }
            public string Subtitle { get; set; }
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

        [Fact]
        public void FormatSampleFeedWithBlueShedSampleExtension()
        {
            var feed = new JsonFeed
            {
                Items = new List<JsonFeedItem>
                {
                    new JsonFeedItem { Id = "abc" },
                },
                Extensions =
                {
                    new BlueShedSampleExtension
                    {
                        About = "https://blueshed-podcasts.com/json-feed-extension-docs",
                        Explicit = false,
                        Copyright = "1948 by George Orwell",
                        Owner = "Big Brother and the Holding Company",
                        Subtitle = "All shouting, all the time. Double. Plus. Good.",
                    }
                }
            };

            var tryFormatResult = JsonFeedFormatter.TryFormatJsonFeed(feed, out var document, new ExtensionManifestDirectory { new BlueShedSampleExtensionManifest() });
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
                Assert.Contains("_blue_shed", jsonString);
                Assert.NotEmpty(jsonString);
            }
        }

        [Theory]
        [ClassData(typeof(ParseSampleFeedWithBlueShedSampleExtensionData))]
        public void ParseSampleFeedWithBlueShedSampleExtension(SampleFeed embeddedDocument)
        {
            // arrange
            var document1 = embeddedDocument.JsonDocument;

            // action
            var tryParseResult = JsonFeedParser.TryParseJsonFeed(document1, out var feed, new ExtensionManifestDirectory { new BlueShedSampleExtensionManifest() });
            Assert.True(tryParseResult);

            // assert
            Assert.Equal(1, feed.Extensions.Count);
            var blueShedExtension = (BlueShedSampleExtension)feed.Extensions.SingleOrDefault(x => x is BlueShedSampleExtension);
            Assert.NotNull(blueShedExtension);
            Assert.Equal("https://blueshed-podcasts.com/json-feed-extension-docs", blueShedExtension.About);
        }

        public class ParseSampleFeedWithBlueShedSampleExtensionData : SampleFeedTestsClassDataBase
        {
            public override IEnumerable<string> FileNames => new[]
            {
                "_jsonfeed1-sample02.json",
            };
        }
    }
}
