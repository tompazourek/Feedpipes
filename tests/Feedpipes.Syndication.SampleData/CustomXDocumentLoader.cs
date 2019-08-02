using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Feedpipes.Syndication.SampleData
{
    public static class CustomXDocumentLoader
    {
        public static XDocument LoadFromString(string inputString)
        {
            var preprocessedStringBuilder = new StringBuilder(inputString.Length);
            var soFarOnlyWhitespace = true; // flag to do left whitespace trimming
            foreach (var c in inputString)
            {
                if (soFarOnlyWhitespace && char.IsWhiteSpace(c))
                    continue;

                if (char.IsControl(c) && c != '\n' && c != '\r' && c != '\t')
                {
                    if (soFarOnlyWhitespace)
                        continue;

                    preprocessedStringBuilder.Append(' ');
                    continue;
                }

                soFarOnlyWhitespace = false;
                preprocessedStringBuilder.Append(c);
            }

            var processedXmlString = preprocessedStringBuilder.ToString();

            using (var stringReader = new StringReader(processedXmlString))
            {
                var document = XDocument.Load(stringReader);
                return document;
            }
        }

        public static XDocument LoadFromStream(Stream inputStream)
        {
            using (var streamReader = new StreamReader(inputStream))
            {
                return LoadFromString(streamReader.ReadToEnd());
            }
        }
    }
}