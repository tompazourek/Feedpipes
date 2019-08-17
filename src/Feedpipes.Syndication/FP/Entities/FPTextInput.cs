using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;

namespace Feedpipes.Syndication.FP.Entities
{
    public class FPTextInput : IRssTextInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}