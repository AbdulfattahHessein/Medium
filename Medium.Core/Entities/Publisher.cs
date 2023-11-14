using Medium.Core.Constants;

namespace Medium.Core.Entities
{
    public class Publisher : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; } = Defaults.ProfilePhotoPath;
        public ICollection<Story> Stories { get; set; } = new HashSet<Story>();
        public ICollection<Publisher> Followers { get; set; } = new HashSet<Publisher>();
        public ICollection<Publisher> Followings { get; set; } = new HashSet<Publisher>();
        public ICollection<SavingList> SavingLists { get; set; } = new HashSet<SavingList>();
        public ICollection<React> Reacts { get; set; } = new HashSet<React>();
        public ApplicationUser User { get; set; }
    }
}
