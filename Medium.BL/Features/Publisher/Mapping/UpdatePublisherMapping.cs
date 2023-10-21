using AutoMapper;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
using Entities = Medium.Core.Entities;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile : Profile
    {
        void UpdatePublisherMapping()
        {
            CreateMap<Entities.Publisher, UpdatePublisherResponse>()
                .ReverseMap();
            CreateMap<Entities.Publisher, UpdatePublisherRequest>()
                .ReverseMap();
        }
    }
}
