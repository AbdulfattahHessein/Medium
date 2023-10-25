using Medium.Core.Entities;

namespace Medium.BL.Features.SavingLists.Response
{
    public record GetSavingListWithStoriesResponse(int Id, string Name, List<Story> Stories);

}
