using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Feedpipes.Utils.Xml
{
    public static class RelaxedXDocumentLoader
    {
        public static XDocument LoadFromString(string inputString)
        {
            var preprocessedStringBuilder = new StringBuilder(inputString.Length);
            var soFarOnlyWhitespace = true;
            foreach (var c in inputString)
            {
                // left whitespace trimming
                if (soFarOnlyWhitespace && char.IsWhiteSpace(c))
                    continue;

                // replace control characters with a space
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
