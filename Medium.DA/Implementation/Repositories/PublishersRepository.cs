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

        public Task<List<Publisher>> GetAllFollowers(int publisherId, int? skip, int? take)
        {
            //var query = _table.Include(p => p.Followers).Where(p => p.Id == publisherId); 
            var query = _table.Where(p => p.Id == publisherId).SelectMany(p => p.Followers);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query.ToListAsync();

        }

        public Task<List<Publisher>> GetAllFollowings(int publisherId, int? skip, int? take)
        {
            var query = _table.Where(p => p.Id == publisherId).SelectMany(p => p.Followings);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query.ToListAsync();

        }

        public Task<List<Publisher>> GetFollowersNotFollowings(int publisherId, int? skip, int? take)
        {
            var query = _table.Where(p => p.Id == publisherId).SelectMany(p => p.Followers.Except(p.Followings));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query.ToListAsync();
        }
    }

}
