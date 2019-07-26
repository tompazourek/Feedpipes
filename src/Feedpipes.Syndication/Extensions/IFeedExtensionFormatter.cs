using System.Xml.Linq;

namespace Feedpipes.Syndication.Extensions
{
    public interface IFeedExtensionFormatter
    {
        string GetNamespaceAlias();
        XNamespace GetNamespace();
        bool TryFormatExtensionEntity(IFeedExtensionEntity extensionEntityToFormat, out XElement element);
    }
}