using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;

namespace Medium.BL.AppServices
{
    public class StoriesService : IStoriesService
    {
        public Task<ApiResponse<CreateStoryResponse>> CreateStory(Story story)
        {
            throw new NotImplementedException();
        }
    }
}
