namespace Medium.Core.Entities
{
    public class Story : Entity<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<StoryPhoto> StoryPhotos { get; set; }
        public ICollection<StoryVideo> StoryVideos { get; set; }
        public ICollection<SavingList> SavingLists { get; set; } = new HashSet<SavingList>();
        public ICollection<React>? Reacts { get; set; }

        public Story(int id)
        {
            Id = id;
        }
        public Story()
        {
            Title = string.Empty;
            Content = string.Empty;
            Publisher = new Publisher();
        }
        public Story(string title, string content, Publisher publisher)
        {
            Title = title;
            Content = content;
            Publisher = publisher;
        }
    }
}
