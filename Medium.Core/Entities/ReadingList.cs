namespace Medium.Core.Entities
{
    public class SavingList : Entity<int>
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        public ICollection<Story> Stories { get; set; }
    }
}
