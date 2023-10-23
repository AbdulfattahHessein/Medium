using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
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
    public class ReactsRepository : Repository<React, int>, IReactsRepository
    {
        public ReactsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
