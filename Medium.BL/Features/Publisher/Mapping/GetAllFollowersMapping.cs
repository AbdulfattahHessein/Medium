using Medium.BL.Features.Publisher.Response;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile
    {
        void GetAllFollowersMapping()
        {
            CreateMap<Core.Entities.Publisher, GetAllFollowersResponse>();

        }
    }
}
