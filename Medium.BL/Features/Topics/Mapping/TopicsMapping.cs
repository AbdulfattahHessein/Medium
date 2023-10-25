using AutoMapper;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Features.Topics.Response;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;

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
            CreateMap<Topic, GetAllPaginationTopicResponse>();
        }
    }
}
