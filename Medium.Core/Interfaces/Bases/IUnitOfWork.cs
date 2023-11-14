using Medium.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Medium.Core.Interfaces.Bases
{
    public interface IUnitOfWork
    {
        public IStoriesRepository Stories { get; }
        public IPublishersRepository Publishers { get; }
        public IReactionsRepository Reactions { get; }
        public ITopicsRepository Topics { get; }
        public IReactsRepository Reacts { get; }
        public IStoryPhotoRepository StoryPhotos { get; }
        public IStoryVideoRepository StoryVideos { get; }
        public ISavingListRepository SavingLists { get; }
        Task<int> CommitAsync();
        public void Dispose();
        public EntityEntry Attach(object entity);
    }
}
