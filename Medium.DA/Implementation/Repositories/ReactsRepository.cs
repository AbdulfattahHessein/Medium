using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class ReactsRepository : Repository<React, int>, IReactsRepository
    {
        public ReactsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
