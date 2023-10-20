using AutoMapper;
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
        }
        void GetAllPublisherMapping()
        {
            CreateMap<Entities.Publisher, GetAllPublisherResponse>();
        }
    }
}
