namespace Medium.Core.Entities
{
    public class Reaction : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<React>? Reacts { get; set; }

    }
}
