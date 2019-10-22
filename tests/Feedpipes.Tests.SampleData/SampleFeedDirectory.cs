using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Csv;
using Feedpipes.Syndication.Utils.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable 168

namespace Feedpipes.Syndication.SampleData
{
    public static class SampleFeedDirectory
    {
        private static readonly Assembly _currentAssembly;
        private static readonly string _manifestResourceStreamPrefix;

        static SampleFeedDirectory()
        {
            var currentType = typeof(SampleFeedDirectory);
            _currentAssembly = currentType.GetTypeInfo().Assembly;
            _manifestResourceStreamPrefix = currentType.Namespace + ".";
        }

        public static IEnumerable<SampleFeed> GetSampleFeeds()
        {
            using (var csvStream = _currentAssembly.GetManifestResourceStream(_manifestResourceStreamPrefix + "files-metadata.csv"))
            {
                var csvOptions = new CsvOptions
                {
                    HeaderMode = HeaderMode.HeaderPresent,
                };

                foreach (var line in CsvReader.ReadFromStream(csvStream, csvOptions))
                {
                    var feed = new SampleFeed
                    {
                        FileName = line["FileName"],
                        FeedUrl = line["FeedUrl"],
                        Title = line["Title"],
                        WebUrl = line["WebUrl"],
                        Source = line["Source"],
                    };

                    if (feed.FileName.EndsWith(".xml"))
                    {
                        feed.LazyXDocument = new Lazy<XDocument>(() =>
                        {
                            var streamName = $"{_manifestResourceStreamPrefix}Files.{feed.FileName}";
                            using (var feedStream = _currentAssembly.GetManifestResourceStream(streamName))
                            {
                                if (feedStream == null)
                                    throw new ArgumentNullException($"Couldn't find manifest resource stream '{streamName}'.");

                                try
                                {
                                    return RelaxedXDocumentLoader.LoadFromStream(feedStream);
                                }

                                catch (XmlException ex)
                                {
                                    Debugger.Break();
                                    throw;
                                }
                            }
                        });
                    }
                    else if (feed.FileName.EndsWith(".json"))
                    {
                        feed.LazyJsonDocument = new Lazy<JObject>(() =>
                        {
                            var streamName = $"{_manifestResourceStreamPrefix}Files.{feed.FileName}";
                            using (var feedStream = _currentAssembly.GetManifestResourceStream(streamName))
                            {
                                if (feedStream == null)
                                    throw new ArgumentNullException($"Couldn't find manifest resource stream '{streamName}'.");

                                try
                                {
                                    using (var streamReader = new StreamReader(feedStream))
                                    using (var jsonReader = new JsonTextReader(streamReader))
                                    {
                                        var jsonDocument = JObject.Load(jsonReader, new JsonLoadSettings
                                        {
                                            CommentHandling = CommentHandling.Ignore,
                                            DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace,
                                            LineInfoHandling = LineInfoHandling.Load,
                                        });
                                        return jsonDocument;
                                    }
                                }
                                catch (JsonException ex)
                                {
                                    Debugger.Break();
                                    throw;
                                }
                            }
                        });
                    }
                    else
                    {
                        throw new Exception($"Cannot parse file '{feed.FileName}'.");
                    }

                    yield return feed;
                }
            }
        }
    }
}