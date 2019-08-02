using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Csv;

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

                    feed.SetDocumentFactory(() =>
                    {
                        var streamName = $"{_manifestResourceStreamPrefix}Files.{feed.FileName}.xml";
                        using (var feedStream = _currentAssembly.GetManifestResourceStream(streamName))
                        {
                            if (feedStream == null)
                                throw new ArgumentNullException($"Couldn't find manifest resource stream '{streamName}'.");

                            try
                            {
                                return CustomXDocumentLoader.LoadFromStream(feedStream);
                            }
                            catch (XmlException ex)
                            {
                                Debugger.Break();
                                throw;
                            }
                        }
                    });

                    yield return feed;
                }
            }
        }
    }
}