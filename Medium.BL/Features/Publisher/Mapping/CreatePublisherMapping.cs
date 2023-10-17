using AutoMapper;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
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
        void CreatePublisherMapping()
        {
            CreateMap<Entities.Publisher, CreatePublisherResponse>();
        }
    }
}
