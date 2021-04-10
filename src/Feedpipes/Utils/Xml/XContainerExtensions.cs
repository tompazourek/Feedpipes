using System.Collections.Generic;
using System.Xml.Linq;

namespace Feedpipes.Utils.Xml
{
    internal static class XContainerExtensions
    {
        public static void AddRange(this XContainer container, IEnumerable<object> nodes)
        {
            foreach (var node in nodes)
            {
                container.Add(node);
            }
        }
    }
}
