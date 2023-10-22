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


                public record CreateTopicRequest(string Name);
                public record CreateTopicResponse(int Id, string Name);

                public record UpdateTopicRequest(int Id, string Name);
                public record UpdateTopicResponse(int Id, string Name);

                public record DeleteTopicRequest(int Id);
                public record DeleteTopicResponse(int Id, string Name);

                public record GetTopicByIdRequest(int Id);
                public record GetTopicByIdResponse(int Id, string Name);
}
