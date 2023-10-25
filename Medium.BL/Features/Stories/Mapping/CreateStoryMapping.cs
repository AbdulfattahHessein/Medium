using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void CreateStoryMapping()
        {
            CreateMap<Story, CreateStoryResponse>()
                 .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos))
                .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos));
            CreateMap<Story, CreateStoryRequest>()
                 .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos))
                .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos));
        }
    }
}
