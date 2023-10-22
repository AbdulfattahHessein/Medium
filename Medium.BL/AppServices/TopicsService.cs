using AutoMapper;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Features.Topics.Response;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using static Medium.BL.ResponseHandler.ApiResponseHandler;
namespace Medium.BL.AppServices
{
    public class TopicsService : AppService, ITopicsService
    {
        public TopicsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<ApiResponse<CreateTopicResponse>> CreateAsync(CreateTopicRequest requset)
        {
            Topic Topic = new Topic() { Name = requset.Name };
            await UnitOfWork.Topics.InsertAsync(Topic);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreateTopicResponse>(Topic);

            return Success(response);

        }

        public async Task<ApiResponse<DeleteTopicResponse>> DeleteAsync(DeleteTopicRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<DeleteTopicResponse>();

            }

            UnitOfWork.Topics.Delete(Topic);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<DeleteTopicResponse>(Topic);
            return Success(response);

        }

        public async Task<ApiResponsePaginated<List<GetAllPaginationTopicResponse>>> GetAllAsync(GetAllPaginationTopicRequest request)
        {
            var topics = await UnitOfWork.Topics
                 .GetAllAsync(s => s.Name.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var totalCount = await UnitOfWork.Topics.CountAsync((s => s.Name.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationTopicResponse>>(topics);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<ApiResponse<GetTopicByIdResponse>> GetById(GetTopicByIdRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<GetTopicByIdResponse>();
            }

            var response = Mapper.Map<GetTopicByIdResponse>(Topic);
            return Success(response);
        }

        public async Task<ApiResponse<UpdateTopicResponse>> UpdateAsync(UpdateTopicRequest requset)
        {
            var Topic = await UnitOfWork.Topics.GetByIdAsync(requset.Id);
            if (Topic == null)
            {
                return NotFound<UpdateTopicResponse>();
            }
            Mapper.Map(requset, Topic);
            UnitOfWork.Topics.Update(Topic);
            await UnitOfWork.CommitAsync();
            var response = Mapper.Map<UpdateTopicResponse>(Topic);
            return Success(response);
        }
    }
}
