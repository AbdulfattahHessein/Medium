
using Medium.BL.Features.Publisher.Responses;

namespace Medium.BL.Features.Publisher.Mapping
{
    public partial class PublisherProfile
    {
        void DeleteFollowingResponseMapping()
        {
            CreateMap<Core.Entities.Publisher, DeleteFollowingResponse>();
        }
    }
}