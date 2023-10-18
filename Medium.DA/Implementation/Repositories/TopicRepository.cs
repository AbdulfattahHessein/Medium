using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.DA.Implementation.Repositories
{
    public class TopicsRepository : Repository<Topic, int>, ITopicsRepository
    {
        public TopicsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
