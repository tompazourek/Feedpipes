using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Feedpipes.Tests.SampleData
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

        public JObject JsonDocument => LazyJsonDocument?.Value;
        internal Lazy<JObject> LazyJsonDocument { get; set; }
    }
}
