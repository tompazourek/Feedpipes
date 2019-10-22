using System.Collections.Generic;
using JetBrains.Annotations;

namespace Feedpipes.Syndication.Extensions
{
    public interface IExtensibleEntity
    {
        [NotNull]
        IList<IExtensionEntity> Extensions { get; }
    }
}
