namespace Medium.Core.Entities
{
    public class SavingList : Entity<int>
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
