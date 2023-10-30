using Medium.BL.Features.Reactions.Request;
using Medium.BL.Features.Reactions.Response;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IReactionsService
    {
        Task<ApiResponse<CreateReactionResponse>> CreateAsync(CreateReactionRequest requset);
        Task<ApiResponse<UpdateReactionResponse>> UpdateAsync(UpdateReactionRequest requset);
        Task<ApiResponse<DeleteReactionResponse>> DeleteAsync(DeleteReactionRequest requset);
        Task<ApiResponse<GetReactionByIdResponse>> GetById(GetReactionByIdRequest requset);
        Task<ApiResponsePaginated<List<GetAllPaginationReactionsResponse>>> GetAllAsync(GetAllPaginationReactionsRequest request);
        Task<ApiResponse> AddReactToStory(AddReactToStoryRequest request, int publisherId);
        Task<ApiResponse<RemoveReactFromStoryResponse>> RemoveReactFromStory(RemoveReactFromStoryRequest request);

    }
}
