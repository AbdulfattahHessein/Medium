using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class StoryPhotoRepository : Repository<StoryPhoto, int>, IStoryPhotoRepository
    {
        public StoryPhotoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
