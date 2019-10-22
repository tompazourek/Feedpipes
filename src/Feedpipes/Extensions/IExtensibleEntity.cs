using System.Collections.Generic;
using JetBrains.Annotations;

namespace Feedpipes.Extensions
{
    public interface IExtensibleEntity
    {
        [NotNull]
        IList<IExtensionEntity> Extensions { get; }
    }
}
