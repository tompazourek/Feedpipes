using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;

namespace Feedpipes.Syndication.Extensions
{
    public class AbstractFeedExtensionEntityParser
    {
        private readonly IEnumerable<IFeedExtensionParser> _feedExtensionParsers;

        public AbstractFeedExtensionEntityParser()
        {
            _feedExtensionParsers = new IFeedExtensionParser[]
            {
                new Rss10ContentExtensionParser(),
                new Rss10SyndicationExtensionParser(),
                new Rss10SlashExtensionParser(),
            };
        }

        public IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement)
        {
            if (parentElement == null)
                yield break;

            foreach (var extensionParser in _feedExtensionParsers)
            {
                foreach (var result in extensionParser.ParseExtensionEntities(parentElement))
                {
                    yield return result;
                }
            }
        }
    }
}