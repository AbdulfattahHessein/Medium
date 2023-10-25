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
        public IReactionsRepository Reactions { get; }
        public ITopicsRepository Topics { get; }
        public IReactsRepository Reacts { get; }
        public IStoryPhotoRepository StoryPhotos { get; }
        public IStoryVideoRepository StoryVideos { get; }
        public ISavingListRepository SavingLists { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            Stories = new StoryRepository(dbContext);
            Publishers = new PublishersRepository(dbContext);
            Reactions = new ReactionsRepository(dbContext);
            Topics = new TopicsRepository(dbContext);
            Reacts = new ReactsRepository(dbContext);
            StoryPhotos = new StoryPhotoRepository(dbContext);
            StoryVideos = new StoryVideoRepository(dbContext);
            SavingLists = new SavingListRepository(dbContext);

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
