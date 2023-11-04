using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Stories.Requests
{
    // public record UpdateStoryRequest(int Id, string Title, string Content, DateTime CreationDate);
    public record UpdateStoryRequest(int Id, string Title, string Content, List<IFormFile>? StoryPhotos, List<IFormFile>? StoryVideos, List<string> Topics);

}
