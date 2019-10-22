using System.Collections.Generic;
using Feedpipes.Syndication.Extensions;

namespace Feedpipes.Syndication.FP.Entities
{
    public class FPImage : IExtensibleEntity
    {
        public string Alt { get; set; }
        public string Tooltip { get; set; }
        public string Url { get; set; }
        public string TargetLinkUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public FPImageKind? Kind { get; set; }
        
        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}