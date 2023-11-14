namespace Medium.BL.Features.Stories.Requests
{
    //  public record GetAllPaginationStoryRequest(int PageNumber = 1, int PageSize = 10, string Search = "");
    public class GetAllPaginationStoryRequest
    {
        //  public string TopicName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

}
