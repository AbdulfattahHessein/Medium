using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void UpdateStoryMapping()
        {
            //CreateMap<Story, UpdateStoryResponse>().ReverseMap();
            //CreateMap<Story, UpdateStoryRequest>()
            //      .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos))
            //    .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos));
            // CreateMap<UpdateStoryRequest, Story>()

            CreateMap<Story, UpdateStoryResponse>().ReverseMap();
            CreateMap<UpdateStoryRequest, Story>()
                .ForMember(dest => dest.StoryPhotos, opt => opt.Ignore())
                .ForMember(dest => dest.StoryVideos, opt => opt.Ignore());






        }
    }
}
