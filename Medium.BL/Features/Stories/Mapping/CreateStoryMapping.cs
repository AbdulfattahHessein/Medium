using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void CreateStoryMapping()
        {
            CreateMap<Story, CreateStoryResponse>()
                .ForMember(s => s.StoryPhotos, options => options.MapFrom(src => src.StoryPhotos.Select(photo => photo.Url)))
                .ForMember(s => s.StoryVideos, options => options.MapFrom(src => src.StoryVideos.Select(video => video.Url)))
                .ForMember(s => s.Topics, options => options.MapFrom(s => s.Topics.Select(t => t.Name)));

            //CreateMap<Story, CreateStoryRequest>()
            //     .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos))
            //    .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos));
        }
    }
}
