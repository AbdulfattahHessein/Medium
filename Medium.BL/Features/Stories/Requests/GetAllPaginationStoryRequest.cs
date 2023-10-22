namespace Medium.BL.Features.Stories.Requests
{
    public record GetAllPaginationStoryRequest(int PageNumber = 1, int PageSize = 10, string Search = "");

}
