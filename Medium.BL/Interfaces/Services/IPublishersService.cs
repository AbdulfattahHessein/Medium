using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;

namespace Medium.BL.Interfaces.Services
{
    public interface IPublishersService
    {
        Task<ApiResponse<CreatePublisherResponse>> CreatePublisherAsync(CreatePublisherRequest request);
        Task<ApiResponse<GetPublisherByIdResponse>> GetPublisherById(GetPublisherByIdRequest request);
        Task<ApiResponse<UpdatePublisherResponse>> UpdatePublisherAsync(UpdatePublisherRequest request);
        Task<ApiResponse<DeletePublisherResponse>> DeletePublisherAsync(DeletePublisherRequest request);
    }
}
