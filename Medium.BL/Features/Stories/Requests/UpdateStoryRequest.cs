namespace Medium.BL.Features.Stories.Requests
{
    public record UpdateStoryRequest(int Id, string Title, string Content, DateTime CreationDate);

}
