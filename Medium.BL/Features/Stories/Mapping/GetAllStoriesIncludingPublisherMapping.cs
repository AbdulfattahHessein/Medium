using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllStoriesIncludingPublisherMapping()
        {
            CreateMap<Story, GetAllStoryIncludePublisherResponse>().
                ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));


        }
    }
}
