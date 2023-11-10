using AutoMapper;
using Medium.BL.Features.Publisher.Response;
using Medium.BL.Interfaces.Services;
using Entities = Medium.Core.Entities;


namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            GetAllPublisherMapping();
            CreatePublisherMapping();
            GetPublisherByIdMapping();
            UpdatePublisherMapping();
            DeletePublisherMapping();
            AddFollowingResponseMapping();
            DeleteFollowingResponseMapping();
            GetFollowerNotFollowingResponseMapping();
            GetAllFollowersMapping();
        }
        void GetAllPublisherMapping()
        {
            CreateMap<Entities.Publisher, GetAllPublisherResponse>();
        }
    }
}
