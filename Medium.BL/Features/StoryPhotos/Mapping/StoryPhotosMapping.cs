using AutoMapper;
using Medium.BL.Features.StoryPhotos.Requests;
using Medium.BL.Features.StoryPhotos.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.StoryPhotos.Mapping
{
    public partial class StoryPhotosProfile : Profile
    {
        void StoryPhotosMapping()
        {
            CreateMap<StoryPhoto, CreateStoryPhotoResponse>();
            CreateMap<StoryPhoto, CreateStoryPhotoRequest>();

        }
    }
}
