using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;

namespace Medium.BL.Interfaces.Services
{
    public interface IStoriesService
    {
        Task<ApiResponse<CreateStoryResponse>> CreateStory(Story story);
    }
}
