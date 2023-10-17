namespace Medium.Core.Entities
{
    public class Publisher : Entity<int>
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<Story>? Stories { get; set; }
        public Publisher()
        {
            Name = string.Empty;
        }
        public Publisher(string name, string? photoUrl = null)
        {
            Name = name;
            PhotoUrl = photoUrl;
        }
        public Publisher(string name, string? bio, string? photoUrl = null) : this(name, photoUrl)
        {
            PhotoUrl = photoUrl;
        }
    }
}
