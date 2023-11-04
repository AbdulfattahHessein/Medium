using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllStoryMapping()
        {
            //CreateMap<Story, GetAllStoryResponse>().ReverseMap()
            //     .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos)).ReverseMap()
            //    .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos)).ReverseMap();

            //CreateMap<Story, GetAllStoryRequest>().ReverseMap();

            //     CreateMap<Story, GetAllStoryResponse>()
            //.ForMember(s => s.StoryPhotos, options => options.MapFrom(src => src.StoryPhotos.Select(photo => photo.Url)))
            //.ForMember(s => s.StoryVideos, options => options.MapFrom(src => src.StoryVideos.Select(video => video.Url).ToList()));

            CreateMap<Story, GetAllStoryResponse>()
             .ForMember(s => s.StoryPhotos, options => options.MapFrom(src => src.StoryPhotos.Select(photo => photo.Url)))
            .ForMember(s => s.StoryVideos, options => options.MapFrom(src => src.StoryVideos.Select(video => video.Url)));
            //.ForMember(s => s.TopicsNames, options => options.MapFrom(s => s.Topics.Select(t => t.Name))); 
        }
    }
}
