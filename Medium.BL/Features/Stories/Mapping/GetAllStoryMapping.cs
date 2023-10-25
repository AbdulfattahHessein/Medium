using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllStoryMapping()
        {
            CreateMap<Story, GetAllStoryResponse>().ReverseMap()
                 .ForMember(s => s.StoryPhotos, options => options.MapFrom(s => s.StoryPhotos)).ReverseMap()
                .ForMember(s => s.StoryVideos, options => options.MapFrom(s => s.StoryVideos)).ReverseMap();

            CreateMap<Story, GetAllStoryRequest>().ReverseMap();


        }
    }
}
