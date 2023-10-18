using AutoMapper;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Medium.Core.Entities;

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

        }
    }
}
