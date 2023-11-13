using Medium.Core.Entities;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medium.DA.Implementation.Repositories
{
    public class StoryRepository : Repository<Story, int>, IStoriesRepository
    {
        public StoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Task<List<Story>> GetAllStoriesByTopicNameAsync(int topicId, int? skip, int? take)
        {
            var query = _dbContext.Topics
                .Where(t => t.Id == topicId)
                .SelectMany(s => s.Stories);


            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            // query.Include(s => s.StoryPhotos);
            // query.Include(s => s.StoryVideos);            
            return query
                .Include(s => s.Publisher)
                .Include(s => s.StoryVideos)
                .Include(s => s.StoryPhotos)
                .Include(s => s.Publisher.Followers)
                .ToListAsync();
        }
        public List<Story> GetStoriesIncludingPublisher(params Expression<Func<Story, object>>[] includes)
        {
            return GetAll(includes);
        }


    }

}
