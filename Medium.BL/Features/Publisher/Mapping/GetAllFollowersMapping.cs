using Medium.BL.Features.Publisher.Response;
using Medium.BL.Features.Publisher.Responses;

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
