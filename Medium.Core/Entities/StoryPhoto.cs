using Medium.Core.Interfaces;

namespace Medium.Core.Entities
{
    public class StoryPhoto : Entity<int>, Resource
    {
        public string Url { get; set; }

    }
}
