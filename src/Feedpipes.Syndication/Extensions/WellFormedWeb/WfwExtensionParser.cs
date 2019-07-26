using System.Collections.Generic;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    public class WfwExtensionParser : IFeedExtensionParser
    {
        public IEnumerable<IFeedExtensionEntity> ParseExtensionEntities(XElement parentElement)
        {
            if (parentElement == null)
                yield break;

            foreach (var element in parentElement.Elements(WfwExtensionConstants.Namespace + "comment"))
            {
                if (!TryParseWfwComment(element, out var entity))
                    continue;

                yield return entity;
            }

            foreach (var element in parentElement.Elements(WfwExtensionConstants.Namespace + "commentRss"))
            {
                if (!TryParseWfwCommentRss(element, out var entity))
                    continue;

                yield return entity;
            }

            // Early in specification, there was a typo that incorrectly named the comment feed element, this handles the scenario where publisher used incorrect element name
            foreach (var element in parentElement.Elements(WfwExtensionConstants.Namespace + "commentRSS"))
            {
                if (!TryParseWfwCommentRss(element, out var entity))
                    continue;

                yield return entity;
            }
        }

        private bool TryParseWfwComment(XElement element, out WfwComment entity)
        {
            entity = default;

            if (element == null)
                return false;

            entity = new WfwComment { Value = element.Value.Trim() };
            return true;
        }

        private bool TryParseWfwCommentRss(XElement element, out WfwCommentRss entity)
        {
            entity = default;

            if (element == null)
                return false;

            entity = new WfwCommentRss { Value = element.Value.Trim() };
            return true;
        }
    }
}