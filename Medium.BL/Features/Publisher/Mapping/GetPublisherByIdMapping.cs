using AutoMapper;
using Medium.BL.Features.Publisher.Responses;
using Entities = Medium.Core.Entities;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile : Profile
    {
        void GetPublisherByIdMapping()
        {
            CreateMap<Entities.Publisher, GetPublisherByIdResponse>()
                 .ForMember(s => s.FollowersCount, options => options.MapFrom(s => s.Followers.Count()));

        }
    }
}
