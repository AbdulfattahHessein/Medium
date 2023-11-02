namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllPaginationStoryResponse(int Id, string Title, string Content, DateTime CreationDate, string PublisherName, string PublisherPhotoUrl, List<string> TopicsNames);

}


