namespace Medium.BL.Features.Reactions.Request
{
    public record GetAllPaginationReactionsRequest(int PageNumber = 1, int PageSize = 10, string Search = "");

}
