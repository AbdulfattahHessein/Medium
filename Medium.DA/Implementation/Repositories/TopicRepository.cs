using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class TopicsRepository : Repository<Topic, int>, ITopicsRepository
    {
        public TopicsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
