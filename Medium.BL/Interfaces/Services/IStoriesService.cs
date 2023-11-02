using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IStoriesService
    {
        Task<ApiResponse<CreateStoryResponse>> CreateStoryAsync(CreateStoryRequest request, int publisherId);
        Task<ApiResponse<GetStoryByIdResponse>> GetStoryById(GetStoryByIdRequest request);
        Task<ApiResponse<UpdateStoryResponse>> UpdateStory(UpdateStoryRequest request);
        Task<ApiResponse<DeleteStoryResponse>> DeleteStoryAsync(DeleteStoryRequest request);
        Task<ApiResponse<List<GetAllStoryIncludePublisherResponse>>> GetAllStoriesIncludingPublisher();
        Task<ApiResponse<List<GetAllStoryResponse>>> GetAllStories();
        Task<ApiResponsePaginated<List<GetAllPaginationStoryResponse>>> GetAllAsync(GetAllPaginationStoryRequest request);
    }
}
