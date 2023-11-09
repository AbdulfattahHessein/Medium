using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetStoryByIdMapping()
        {
            CreateMap<Story, GetStoryByIdResponse>()
                      .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos.Select(s => s.Url)))
                      .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos.Select(s => s.Url)))
                      .ForMember(s => s.PublisherId, options => options.MapFrom(s => s.Publisher.Id))
                      .ForMember(s => s.PublisherName, options => options.MapFrom(s => s.Publisher.Name))
                      .ForMember(s => s.PublisherPhoto, options => options.MapFrom(s => s.Publisher.PhotoUrl))
                       .ForMember(s => s.Topics, options => options.MapFrom(s => s.Topics.Select(t => t.Name)))
                       .ForMember(s => s.ReactsCount, options => options.MapFrom(s => s.Reacts.Count()));
        }
    }
}
