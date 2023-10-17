namespace Medium.Core.Entities
{
    public class Story : Entity<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
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
