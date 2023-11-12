using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;

namespace Medium.Core.Interfaces.Repositories
{
    public interface IPublishersRepository : IRepository<Publisher, int>
    {
        Task<List<Publisher>> GetAllFollowers(int publisherId, int? skip, int? take);
        Task<List<Publisher>> GetAllFollowings(int publisherId, int? skip, int? take);
        Task<List<Publisher>> GetFollowersNotFollowings(int publisherId, int? skip, int? take);
    }
}
