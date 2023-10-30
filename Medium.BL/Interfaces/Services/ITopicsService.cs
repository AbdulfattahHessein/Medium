using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Features.Topics.Response;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface ITopicsService
    {
        Task<ApiResponse<CreateTopicResponse>> CreateAsync(CreateTopicRequest requset);
        Task<ApiResponse<UpdateTopicResponse>> UpdateAsync(UpdateTopicRequest requset);
        Task<ApiResponse<DeleteTopicResponse>> DeleteAsync(DeleteTopicRequest requset);
        Task<ApiResponse<GetTopicByIdResponse>> GetById(GetTopicByIdRequest requset);
        Task<ApiResponsePaginated<List<GetAllPaginationTopicResponse>>> GetAllAsync(GetAllPaginationTopicRequest request);
    }



}
