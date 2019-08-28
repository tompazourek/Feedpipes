using System;
using System.Text.Json;
using System.Xml.Linq;

namespace Feedpipes.Syndication.SampleData
{
    public class SampleFeed
    {
        public string FileName { get; set; }
        public string FeedUrl { get; set; }
        public string Title { get; set; }
        public string WebUrl { get; set; }
        public string Source { get; set; }
        
        public override string ToString() => FileName;

        public XDocument XDocument => LazyXDocument?.Value;
        internal Lazy<XDocument> LazyXDocument { get; set; }

        public JsonDocument JsonDocument => LazyJsonDocument?.Value;
        internal Lazy<JsonDocument> LazyJsonDocument { get; set; }
    }
}