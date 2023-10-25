using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;

namespace Medium.DA.Implementation.Repositories
{
    public class SavingListRepository : Repository<SavingList, int>, ISavingListRepository
    {
        public SavingListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
