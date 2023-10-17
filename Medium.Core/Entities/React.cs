using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Core.Entities
{
    public class React
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
