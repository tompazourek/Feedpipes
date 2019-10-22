using System.Collections.Generic;
using Feedpipes.Syndication.Extensions;

namespace Feedpipes.Syndication.FP.Entities
{
    public class FPTextInput : IExtensibleEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        
        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}