namespace Medium.Core.Entities
{
    public abstract class Entity<Key> where Key : struct
    {
        public Key Id { get; set; }
        public Entity()
        {
            Id = default;
        }
    }
}
