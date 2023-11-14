using Medium.BL.Features.Publisher.Responses;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile
    {
        void GetFollowerNotFollowingResponseMapping()
        {
            CreateMap<Core.Entities.Publisher, FollowerNotFollowingResponse>()
                 .ForMember(s => s.IsFollowing, options => options.Ignore());//options.MapFrom(s => s.Followers.Any(f => f.Id == publishersService.PublisherId))); ;


        }
    }
}
