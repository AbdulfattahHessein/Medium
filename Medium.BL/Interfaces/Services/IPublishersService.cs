using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;

namespace Medium.BL.Interfaces.Services
{
    public interface IPublishersService
    {
        Task<ApiResponsePaginated<List<GetAllPublisherResponse>>> GetAllAsync(GetAllPublisherRequest request);
        Task<ApiResponse<CreatePublisherResponse>> Create(CreatePublisherRequest request);
        Task<ApiResponse<GetPublisherByIdResponse>> GetById(GetPublisherByIdRequest request);
        Task<ApiResponse<UpdatePublisherResponse>> UpdateAsync(UpdatePublisherRequest request);
        Task<ApiResponse<DeletePublisherResponse>> DeleteAsync(DeletePublisherRequest request);
    }
    public record GetAllPublisherRequest(int PageNumber = 1, int PageSize = 10, string Search = "");
    public record GetAllPublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
