namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllPaginationStoryResponse(int Id, string Title, string Content, DateTime CreationDate, string PublisherName, string PublisherPhotoUrl, List<string> TopicsNames);
    //public class GetAllPaginationStoryResponse
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }
    //    public DateTime CreationDate { get; set; }
    //    public string PublisherName { get; set; }
    //    public string PublisherPhotoUrl { get; set; }
    //    public List<string> TopicsNames { get; set; }
}


