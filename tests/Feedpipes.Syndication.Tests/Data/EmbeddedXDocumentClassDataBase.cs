using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Feedpipes.Syndication.Tests.Data
{
    public abstract class EmbeddedXDocumentClassDataBase : IEnumerable<object[]>
    {
        public abstract IEnumerable<string> XmlFileNames { get; }

        public IEnumerator<object[]> GetEnumerator()
        {
            return XmlFileNames
                .Select(x => new EmbeddedXDocumentTestData
                {
                    Document = ReadEmbeddedXDocument($"Feedpipes.Syndication.Tests.Data.{x}"),
                    XmlFileName = x,
                })
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private XDocument ReadEmbeddedXDocument(string streamName)
        {
            var currentAssembly = GetType().GetTypeInfo().Assembly;
            using (var stream = currentAssembly.GetManifestResourceStream(streamName))
            {
                var document = XDocument.Load(stream);
                return document;
            }
        }
    }
}