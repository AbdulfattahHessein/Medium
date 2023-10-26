using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Publisher.Validators;
using Medium.BL.Features.Reactions.Request;
using Medium.BL.Features.Reactions.Response;
using Medium.BL.Features.Reactions.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using static Medium.BL.ResponseHandler.ApiResponseHandler;


namespace Medium.BL.AppServices
{
    public class ReactionsService : AppService, IReactionsService
    {
        public ReactionsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ApiResponse> AddReactToStory(AddReactToStoryRequest request)
        {
            //var validator = new AddReactToStoryRequestValidator(UnitOfWork);
            //var validateResult = await validator.ValidateAsync(request);
            //if (!validateResult.IsValid)
            //{
            //    throw new ValidationException(validateResult.Errors);
            //}

            await DoValidationAsync<AddReactToStoryRequestValidator, AddReactToStoryRequest>(request, UnitOfWork);

            var react = new React()
            {
                StoryId = request.StoryId,
                ReactionId = request.ReactionId,
                PublisherId = request.PublisherId,
            };
            await UnitOfWork.Reacts.InsertAsync(react);
            await UnitOfWork.CommitAsync();

            return NoContent<ApiResponse>();
        }
        public async Task<ApiResponse<RemoveReactFromStoryResponse>> RemoveReactFromStory(RemoveReactFromStoryRequest request)
        {
            var react = await UnitOfWork.Reacts.FindAsync(request.StoryId, request.PublisherId);
            //var react = await UnitOfWork.Reacts.GetFirstAsync(r => r.StoryId.Equals(request.storyId) && r.PublisherId.Equals(request.publisherId));

            await DoValidationAsync<RemoveReactFromStoryRequestValidator, RemoveReactFromStoryRequest>(request, UnitOfWork);

            if (react == null)
            {
                return NotFound<RemoveReactFromStoryResponse>();
            }
            UnitOfWork.Reacts.Delete(react);
            await UnitOfWork.CommitAsync();

            return NoContent<RemoveReactFromStoryResponse>();
        }

        public async Task<ApiResponse<CreateReactionResponse>> CreateAsync(CreateReactionRequest requset)
        {
            Reaction reaction = new Reaction() { Name = requset.Name };
            await UnitOfWork.Reactions.InsertAsync(reaction);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreateReactionResponse>(reaction);

            return Success(response);

        }

        public async Task<ApiResponse<DeleteReactionResponse>> DeleteAsync(DeleteReactionRequest request)
        {
            await DoValidationAsync<DeleteReactionRequestValidator, DeleteReactionRequest>(request, UnitOfWork);

            var reaction = await UnitOfWork.Reactions.GetByIdAsync(request.Id)!;
            //if (reaction == null)
            //{
            //    return NotFound<DeleteReactionResponse>();

            //}

            UnitOfWork.Reactions.Delete(reaction);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<DeleteReactionResponse>(reaction);
            return Success(response);

        }

        public async Task<ApiResponsePaginated<List<GetAllPaginationReactionsResponse>>> GetAllAsync(GetAllPaginationReactionsRequest request)
        {
            var reactions = await UnitOfWork.Reactions
                 .GetAllAsync(r => r.Name.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var totalCount = await UnitOfWork.Reactions.CountAsync(r => r.Name.Contains(request.Search));

            var response = Mapper.Map<List<GetAllPaginationReactionsResponse>>(reactions);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<ApiResponse<GetReactionByIdResponse>> GetById(GetReactionByIdRequest requset)
        {
            var reaction = await UnitOfWork.Reactions.GetByIdAsync(requset.Id);
            if (reaction == null)
            {
                return NotFound<GetReactionByIdResponse>();
            }

            var response = Mapper.Map<GetReactionByIdResponse>(reaction);
            return Success(response);
        }

        public async Task<ApiResponse<UpdateReactionResponse>> UpdateAsync(UpdateReactionRequest requset)
        {
            var reaction = await UnitOfWork.Reactions.GetByIdAsync(requset.Id);
            if (reaction == null)
            {
                return NotFound<UpdateReactionResponse>();
            }
            Mapper.Map(requset, reaction);

            UnitOfWork.Reactions.Update(reaction);
            await UnitOfWork.CommitAsync();
            var response = Mapper.Map<UpdateReactionResponse>(reaction);
            return Success(response);
        }
    }
}
