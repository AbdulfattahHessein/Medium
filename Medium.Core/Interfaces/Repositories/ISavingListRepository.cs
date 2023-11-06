using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;

namespace Medium.Core.Interfaces.Repositories
{
    public interface ISavingListRepository : IRepository<SavingList, int>
    {
        Task AddStoryToSaveList(int storyId, int saveListId);
    }
}
