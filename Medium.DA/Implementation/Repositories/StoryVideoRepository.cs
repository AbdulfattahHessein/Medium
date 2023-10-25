using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class StoryVideoRepository : Repository<StoryVideo, int>, IStoryVideoRepository
    {
        public StoryVideoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }

}
