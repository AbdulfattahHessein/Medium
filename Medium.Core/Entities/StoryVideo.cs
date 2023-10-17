using Medium.Core.Interfaces;

namespace Medium.Core.Entities
{
    public class StoryVideo : Entity<int>, Resource
    {
        public string Url { get; set; }

    }
}
