using AutoMapper;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile : Profile
    {
        public StoryProfile()
        {
            GetStoryByIdMapping();
            DeleteStoryMapping();
            UpdateStoryMapping();
            CreateStoryMapping();
            GetAllStoryMapping();
            GetAllStoriesIncludingPublisherMapping();
            GetAllPaginationStoryMapping();

        }
    }
}
