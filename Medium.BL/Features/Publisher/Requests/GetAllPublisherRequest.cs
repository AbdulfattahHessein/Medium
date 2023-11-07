namespace Medium.BL.Features.Publisher.Requests
{
    public record GetAllPublisherRequest(int PageNumber = 1, int PageSize = 10, string Search = "");

}