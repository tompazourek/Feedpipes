namespace Feedpipes.FP.Entities
{
    public class FPPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public FPPersonKind? Kind { get; set; }
    }
}