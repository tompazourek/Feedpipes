using System;
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

        public XDocument Document => _lazyDocument.Value;

        private Lazy<XDocument> _lazyDocument;

        public void SetDocumentFactory(Func<XDocument> documentFactory)
        {
            _lazyDocument = new Lazy<XDocument>(documentFactory);
        }

        public override string ToString() => FileName;
    }
}