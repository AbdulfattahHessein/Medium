namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllStoryIncludePublisherResponse(int Id, string Title, string Content, DateTime CreationDate, string PublisherName);

}
