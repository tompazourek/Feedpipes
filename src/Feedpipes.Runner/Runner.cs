using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Feedpipes.Syndication;
using Serilog;

namespace Feedpipes.Runner
{
    internal class Runner
    {
        public Runner(ILogger log)
        {
            Log = log;
        }

        private ILogger Log { get; }

        public async Task Run(CancellationToken token)
        {
            Log.Information("Hello world!");

            using (var fileStream = File.OpenRead(@"C:\Users\Tom\Desktop\sample.xml"))
            using (var textReader = new StreamReader(fileStream))
            using (var xmlReader = XmlReader.Create(textReader, new XmlReaderSettings { Async = true }))
            {
                var document = await XDocument.LoadAsync(xmlReader, LoadOptions.None, token);
                var parser = new Rss20FeedParser();
                if (parser.TryParseRss20Feed(document, out var feed))
                {
                    var formatter = new Rss20FeedFormatter();
                    if (formatter.TryFormatRss20Feed(feed, out var document2))
                    {
                        parser.TryParseRss20Feed(document2, out var feed2);
                    }
                }
            }
        }
    }
}