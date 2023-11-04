using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllPaginationStoryMapping()
        {
            CreateMap<Story, GetAllPaginationStoryResponse>()
                .ForMember(s => s.PublisherName, options => options.MapFrom(s => s.Publisher.Name))
                .ForMember(s => s.PublisherPhotoUrl, options => options.MapFrom(s => s.Publisher.PhotoUrl))
                .ForMember(s => s.StoryMainPhoto, options => options.MapFrom(s => s.StoryPhotos.FirstOrDefault().Url))
                .ForMember(s => s.TopicsNames, options => options.MapFrom(s => s.Topics.Select(t => t.Name)));
        }
    }
}
