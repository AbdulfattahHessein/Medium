using AutoMapper;
using Medium.BL.Features.Reactions.Response;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;

namespace Medium.BL.Features.Reactions.Mapping
{
    public partial class PublisherProfile : Profile
    {
        void ReactionMapping()
        {
            CreateMap<Reaction, CreateReactionResponse>();
            CreateMap<Reaction, DeleteReactionResponse>();
            CreateMap<Reaction, UpdateReactionResponse>();
            CreateMap<Reaction, GetReactionByIdResponse>();
            CreateMap<UpdateReactionRequest, Reaction>();
            CreateMap<Reaction, GetAllPaginationReactionsResponse>();

        }
    }
}
