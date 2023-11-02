namespace Medium.BL.Features.SavingLists.Request
{
    public record GetAllPaginationSaveListRequest(int PageNumber = 1, int PageSize = 10, string Search = "");

}
