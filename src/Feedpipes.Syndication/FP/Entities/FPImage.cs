using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;

namespace Feedpipes.Syndication.FP.Entities
{
    public class FPImage : IRssImage
    {
        public string Alt { get; set; }
        public string Tooltip { get; set; }
        public string Url { get; set; }
        public string TargetLinkUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public FPImageKind? Kind { get; set; }
        
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}