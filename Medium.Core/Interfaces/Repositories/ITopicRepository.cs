using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Core.Interfaces.Repositories
{
    public interface ITopicsRepository : IRepository<Topic, int>
    {
    }
}
