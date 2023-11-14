using Medium.BL.Features.Publisher.Response;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile
    {
        void GetAllFollowersMapping()
        {
            CreateMap<Core.Entities.Publisher, GetAllFollowersResponse>()
           .ForMember(s => s.IsFollowing, options => options.Ignore());//MapFrom(s => s.Followers.Any(f => f.Id == publishersService.PublisherId)));

        }
    }
}
