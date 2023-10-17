using Medium.Core.Interfaces.Repositories;

namespace Medium.Core.Interfaces.Bases
{
    public interface IUnitOfWork
    {
        public IStoriesRepository Stories { get; }
        public IPublishersRepository Publishers { get; }
        Task<int> CommitAsync();
        public void Dispose();
    }
}
