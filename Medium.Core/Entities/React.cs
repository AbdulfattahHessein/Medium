﻿namespace Medium.Core.Entities
{
    public class React : Entity<int>
    {
        public Publisher Publisher { get; set; }
        public Story Story { get; set; }
        public Reaction Reaction { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int ReactionId { get; set; }
        public int StoryId { get; set; }
        public int PublisherId { get; set; }

    }
}
