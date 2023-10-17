using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;

namespace Medium.Core.Interfaces.Repositories
{
    public interface IStoriesRepository : IRepository<Story, int>
    {
        IQueryable<Story> GetStoriesByPublisherId(int publisherId);
    }
}
