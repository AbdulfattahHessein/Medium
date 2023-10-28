using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;
using Microsoft.EntityFrameworkCore;

namespace Medium.DA.Implementation.Repositories
{
    public class PublishersRepository : Repository<Publisher, int>, IPublishersRepository
    {
        public PublishersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }

}
