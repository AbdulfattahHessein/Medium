using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Response;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IPublishersService
    {
        Task<ApiResponsePaginated<List<GetAllPublisherResponse>>> GetAllAsync(GetAllPublisherRequest request);
        Task<ApiResponsePaginated<List<FollowerNotFollowingResponse>>> GetFollowerNotFollowing(FollowerNotFollowingRequest request,int publisherId);
        Task<ApiResponse<CreatePublisherResponse>> Create(CreatePublisherRequest request);
        Task<ApiResponse<GetPublisherByIdResponse>> GetById(GetPublisherByIdRequest request);
        Task<ApiResponse<UpdatePublisherResponse>> UpdateAsync(UpdatePublisherRequest request);
        Task<ApiResponse<DeletePublisherResponse>> DeleteAsync(DeletePublisherRequest request);
        Task<ApiResponse<AddFollowingResponse>> AddFollowingAsync(AddFollowingRequest request,int PuplisherId);
        Task<ApiResponse<DeleteFollowingResponse>> DeleteFollowingAsync(DeleteFollowingRequest request,int publisherId);
    }
    public record GetAllPublisherRequest(int PageNumber = 1, int PageSize = 10, string Search = "");
    public record GetAllPublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
