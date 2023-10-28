namespace Medium.BL.Features.Stories.Responses
{
    public record UpdateStoryResponse(int Id, string Title, string Content, DateTime CreationDate);
    //public record UpdateStoryResponse(int Id, string Title, string Content, DateTime CreationDate, int PublisherId, List<string>? StoryPhotos, List<string>? StoryVideos);


}
