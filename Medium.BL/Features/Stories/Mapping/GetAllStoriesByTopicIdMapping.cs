using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllStoriesByTopicIdMapping()
        {
            CreateMap<Story, GetAllStoriesByTopicIdResponse>()
               .ForMember(s => s.PublisherName, options => options.MapFrom(s => s.Publisher.Name))
               .ForMember(s => s.TopicNames, options => options.MapFrom(s => s.Topics.Select(t => t.Name)))
                .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos.Select(p => p.Url)))
                .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos.Select(v => v.Url)))
                .ForMember(s => s.PublisherPhoto, options => options.MapFrom(s => s.Publisher.PhotoUrl))
                .ForMember(s => s.PublisherId, options => options.MapFrom(s => s.Publisher.Id))
                .ForMember(s => s.StroriesNumber, options => options.Ignore());

        }
    }
}

