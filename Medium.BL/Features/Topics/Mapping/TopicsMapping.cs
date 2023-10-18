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

namespace Medium.BL.Features.Topics.Mapping
{
    public partial class TopicsProfile : Profile
    {
        void TopicMapping()
        {
            CreateMap<Topic, CreateTopicResponse>();
            CreateMap<Topic, DeleteTopicResponse>();
            CreateMap<Topic, UpdateTopicResponse>();
            CreateMap<Topic, GetTopicByIdResponse>();
            CreateMap<UpdateTopicRequest, Topic>();
        }
    }
}
