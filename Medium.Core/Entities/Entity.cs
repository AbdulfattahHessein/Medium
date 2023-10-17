namespace Medium.Core.Entities
{
    public abstract class Entity<Key> where Key : struct
    {
        public Key Id { get; protected set; }
        public Entity()
        {
            Id = default;
        }
    }
}
