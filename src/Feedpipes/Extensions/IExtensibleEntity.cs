using System.Collections.Generic;

namespace Feedpipes.Extensions
{
    public interface IExtensibleEntity
    {
        IList<IExtensionEntity> Extensions { get; }
    }
}
