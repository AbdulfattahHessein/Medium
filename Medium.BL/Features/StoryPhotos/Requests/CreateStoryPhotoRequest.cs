using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.StoryPhotos.Requests
{
    public record CreateStoryPhotoRequest(IFormFile? Url, int StoryId);

}
