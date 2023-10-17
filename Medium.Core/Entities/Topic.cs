namespace Medium.Core.Entities
{
    public class Topic : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Story> Stories { get; set; }
    }
}
