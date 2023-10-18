using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Reactions.Mapping
{
    public partial class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            ReactionMapping();

        }
    }
}
