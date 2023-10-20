using AutoMapper;
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

        public async Task<ApiResponse<CreateReactionResponse>> CreateAsync(CreateReactionRequest requset)
        {
            Reaction reaction = new Reaction() { Name = requset.Name };
            await UnitOfWork.Reactions.InsertAsync(reaction);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreateReactionResponse>(reaction);

            return Success(response);

        }

        public async Task<ApiResponse<DeleteReactionResponse>> DeleteAsync(DeleteReactionRequest requset)
        {
            var reaction = await UnitOfWork.Reactions.GetByIdAsync(requset.Id);
            if (reaction == null)
            {
                return NotFound<DeleteReactionResponse>();

            }

            UnitOfWork.Reactions.Delete(reaction);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<DeleteReactionResponse>(reaction);
            return Success(response);

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
