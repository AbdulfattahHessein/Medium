using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System.Linq.Expressions;

namespace Medium.Core.Interfaces.Repositories
{
    public interface IPublishersRepository : IRepository<Publisher, int>
    {
    }
}
