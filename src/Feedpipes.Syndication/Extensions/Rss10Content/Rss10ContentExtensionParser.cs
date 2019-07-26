using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    public class Rss10ContentExtensionParser : IFeedExtensionParser
    {
        public IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement)
        {
            if (parentElement == null)
                yield break;

            // parse "content:encoded"
            foreach (var encodedElement in parentElement.Elements(Rss10ContentExtensionConstants.Namespace + "encoded"))
            {
                if (!TryParseRss10ContentEncoded(encodedElement, out var parsedEncoded))
                    continue;

                yield return parsedEncoded;
            }
        }

        private bool TryParseRss10ContentEncoded(XElement encodedElement, out Rss10ContentEncoded parsedEncoded)
        {
            parsedEncoded = default;

            if (encodedElement == null)
                return false;

            parsedEncoded = new Rss10ContentEncoded { Content = encodedElement.Value };
            return true;
        }
    }
}