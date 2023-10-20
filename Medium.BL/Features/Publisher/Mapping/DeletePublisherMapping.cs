using AutoMapper;
using Medium.BL.Features.Publisher.Responses;
using Entities = Medium.Core.Entities;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile : Profile
    {
        void DeletePublisherMapping()
        {
            CreateMap<Entities.Publisher, DeletePublisherResponse>();
        }
    }
}
