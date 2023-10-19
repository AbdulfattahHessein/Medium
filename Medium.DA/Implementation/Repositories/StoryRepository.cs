using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;
using System.Linq.Expressions;

namespace Medium.DA.Implementation.Repositories
{
    public class StoryRepository : Repository<Story, int>, IStoriesRepository
    {
        public StoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<Story> GetStoriesIncludingPublisher(params Expression<Func<Story, object>>[] includes)
        {
            return GetAll(includes);
        }


    }

}
