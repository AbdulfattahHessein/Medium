using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class StoryRepository : Repository<Story, int>, IStoriesRepository
    {
        public StoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Story> GetStoriesByPublisherId(int publisherId)
        {
            return GetAllAsQueryable();//to do
        }
    }

}
