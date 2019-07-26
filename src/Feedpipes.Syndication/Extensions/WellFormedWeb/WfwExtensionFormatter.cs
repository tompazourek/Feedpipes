using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    public class WfwExtensionFormatter : IFeedExtensionFormatter
    {
        public string GetNamespaceAlias() => WfwExtensionConstants.NamespaceAlias;
        public XNamespace GetNamespace() => WfwExtensionConstants.Namespace;

        public bool TryFormatExtensionEntity(IFeedExtensionEntity extensionEntityToFormat, out XElement element)
        {
            element = default;

            if (extensionEntityToFormat == null)
                return false;

            switch (extensionEntityToFormat)
            {
                case WfwComment entity:
                    if (TryFormatWfwComment(entity, out element))
                        return true;
                    break;
                case WfwCommentRss entity:
                    if (TryFormatWfwCommentRss(entity, out element))
                        return true;
                    break;
            }

            return false;
        }

        private bool TryFormatWfwComment(WfwComment entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(WfwExtensionConstants.Namespace + "comment") { Value = entity.Value };

            return true;
        }

        private bool TryFormatWfwCommentRss(WfwCommentRss entity, out XElement element)
        {
            element = default;

            if (entity == null)
                return false;

            element = new XElement(WfwExtensionConstants.Namespace + "commentRss") { Value = entity.Value };

            return true;
        }
    }
}