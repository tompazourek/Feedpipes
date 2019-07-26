using System.Collections.Generic;
using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions
{
    public interface IFeedExtensionParser
    {
        IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement);
    }
}