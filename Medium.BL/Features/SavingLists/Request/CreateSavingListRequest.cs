namespace Medium.BL.Features.SavingLists.Request
{
    public record CreateSavingListRequest(string Name, DateTime? CreationDate = null);

}
