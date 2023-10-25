using Medium.BL.Features.Publisher.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
