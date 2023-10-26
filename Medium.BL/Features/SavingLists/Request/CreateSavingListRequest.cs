namespace Medium.BL.Features.SavingLists.Request
{
    public record CreateSavingListRequest(string Name, int PublisherId, DateTime? CreationDate = null);

}
