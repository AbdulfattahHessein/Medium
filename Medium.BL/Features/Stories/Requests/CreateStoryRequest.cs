namespace Medium.BL.Features.Stories.Requests
{
    public record CreateStoryRequest(string Title, string Content, int PublisherId);
}
