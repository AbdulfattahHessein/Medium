using Medium.BL.Features.Publisher.Response;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile
    {
        void AddFollowingResponseMapping()
        {
            CreateMap<Core.Entities.Publisher, AddFollowingResponse>();
        }
    }
}
