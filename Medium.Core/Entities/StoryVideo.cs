using Medium.Core.Interfaces;

namespace Medium.Core.Entities
{
    public class StoryVideo : Entity<int>, Resource
    {
        public string Url { get; set; }
        public Story Story { get; set; }
        public int StoryId { get; set; }

    }
}
