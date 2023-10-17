using Medium.Core.Interfaces.Bases;
using Medium.Core.Interfaces.Repositories;
using Medium.DA.Context;
using Medium.DA.Implementation.Repositories;

namespace Medium.DA.Implementation.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IStoriesRepository Stories { get; }
        public IPublishersRepository Publishers { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            Stories = new StoryRepository(dbContext);
            Publishers = new PublishersRepository(dbContext);

        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
