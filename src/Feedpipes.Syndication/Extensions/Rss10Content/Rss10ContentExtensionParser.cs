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

            foreach (var element in parentElement.Elements(Rss10ContentExtensionConstants.Namespace + "encoded"))
            {
                if (!TryParseRss10ContentEncoded(element, out var entity))
                    continue;

                yield return entity;
            }
        }

        private bool TryParseRss10ContentEncoded(XElement element, out Rss10ContentEncoded entity)
        {
            entity = default;

            if (element == null)
                return false;

            entity = new Rss10ContentEncoded { Content = element.Value };
            return true;
        }
    }
}