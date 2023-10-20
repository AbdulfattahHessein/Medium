using AutoMapper;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
