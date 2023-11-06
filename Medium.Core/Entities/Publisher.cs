using Microsoft.AspNetCore.Identity;

namespace Medium.Core.Entities
{
    public class Publisher : Entity<int>
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<Story>? Stories { get; set; }
        public ICollection<Publisher>? Followers { get; set; }
        public ICollection<Publisher>? Followings { get; set; }
        public ICollection<SavingList>? SavingLists { get; set; }
        public ICollection<React>? Reacts { get; set; }
        //public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Publisher()
        {
            Name = string.Empty;
        }
        public Publisher(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Publisher(string name, string? photoUrl = null)
        {
            Name = name;
            PhotoUrl = photoUrl;
        }
        public Publisher(string name, string? bio, string? photoUrl = null) : this(name, photoUrl)
        {
            Bio = bio;

        }
    }
}
