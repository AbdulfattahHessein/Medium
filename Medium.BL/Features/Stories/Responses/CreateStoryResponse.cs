namespace Medium.BL.Features.Stories.Responses
{
    public record CreateStoryResponse(int Id, string Title, string Content, DateTime CreationDate, int PublisherId, List<string>? StoryPhotos, List<string>? StoryVideos, string? Topics);


}