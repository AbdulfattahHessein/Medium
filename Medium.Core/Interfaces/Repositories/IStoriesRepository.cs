using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System.Linq.Expressions;

namespace Medium.Core.Interfaces.Repositories
{
    public interface IStoriesRepository : IRepository<Story, int>
    {
        List<Story> GetStoriesIncludingPublisher(params Expression<Func<Story, object>>[] includes);

    }


}
