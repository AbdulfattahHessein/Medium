using Medium.BL.ResponseHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Interfaces.Services
{
    public interface IReactionsService
    {
        Task<ApiResponse<CreateReactionResponse>> CreateAsync(CreateReactionRequest requset);
        Task<ApiResponse<UpdateReactionResponse>> UpdateAsync(UpdateReactionRequest requset);
        Task<ApiResponse<DeleteReactionResponse>> DeleteAsync(DeleteReactionRequest requset);
        Task<ApiResponse<GetReactionByIdResponse>> GetById(GetReactionByIdRequest requset);
        //Task<ApiResponse<GetAllReactionsResponse>> GetAllReactions(GetAllReactionsRequest requset);
    }
    // requests
    public record CreateReactionRequest(string Name);
    public record CreateReactionResponse(int Id, string Name);

    public record UpdateReactionRequest(int Id, string Name);
    public record UpdateReactionResponse(int Id, string Name);

    public record DeleteReactionRequest(int Id);
    public record DeleteReactionResponse(int Id, string Name);

    public record GetReactionByIdRequest(int Id);
    public record GetReactionByIdResponse(int Id, string Name);


    //public record GetAllReactionsRequest(string name);
}
