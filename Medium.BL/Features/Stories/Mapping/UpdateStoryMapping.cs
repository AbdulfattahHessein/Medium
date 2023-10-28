using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void UpdateStoryMapping()
        {
            CreateMap<Story, UpdateStoryResponse>();
            CreateMap<Story, UpdateStoryRequest>();
            CreateMap<UpdateStoryRequest, Story>();




        }
    }
}
