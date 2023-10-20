using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    internal class ReactionsRepository : Repository<Reaction, int>, IReactionsRepository
    {
        public ReactionsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
