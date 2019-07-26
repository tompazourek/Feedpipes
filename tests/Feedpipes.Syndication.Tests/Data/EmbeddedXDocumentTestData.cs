using System.Xml.Linq;

namespace Feedpipes.Syndication.Tests.Data
{
    public class EmbeddedXDocumentTestData
    {
        public string XmlFileName { get; set; }
        public XDocument Document { get; set; }

        public override string ToString() => XmlFileName;
    }
}