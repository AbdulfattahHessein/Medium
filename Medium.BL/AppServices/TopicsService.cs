using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Features.Stories.Validators;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Features.Topics.Response;
using Medium.BL.Features.Topics.Validators;
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
            var validator = new CreateTopicRequestValidator();
            var validateResult = validator.Validate(requset);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            Topic Topic = new Topic() { Name = requset.Name };
            await UnitOfWork.Topics.InsertAsync(Topic);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreateTopicResponse>(Topic);

            return Success(response);

        }

        public async Task<ApiResponse<DeleteTopicResponse>> DeleteAsync(DeleteTopicRequest requset)
        {
            var validator = new DeleteTopicRequestValidator();
            var validateResult = validator.Validate(requset);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
          
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

        public async Task<ApiResponse<GetTopicByIdResponse>> GetById(GetTopicByIdRequest request)
        {
            var validator = new GetTopicByIdRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var Topic = await UnitOfWork.Topics.GetByIdAsync(request.Id);
            if (Topic == null)
            {
                return NotFound<GetTopicByIdResponse>();
            }

            var response = Mapper.Map<GetTopicByIdResponse>(Topic);
            return Success(response);
        }

        public async Task<ApiResponse<UpdateTopicResponse>> UpdateAsync(UpdateTopicRequest request)
        {
            var validator = new UpdateTopicRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var Topic = await UnitOfWork.Topics.GetByIdAsync(request.Id);
            if (Topic == null)
            {
                return NotFound<UpdateTopicResponse>();
            }
            Mapper.Map(request, Topic);
            UnitOfWork.Topics.Update(Topic);
            await UnitOfWork.CommitAsync();
            var response = Mapper.Map<UpdateTopicResponse>(Topic);
            return Success(response);
        }
    }
}
