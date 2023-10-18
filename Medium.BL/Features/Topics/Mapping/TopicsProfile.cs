using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Topics.Mapping
{
    public partial class TopicsProfile : Profile
    {
        public TopicsProfile()
        {
            TopicMapping();
        }
    }
}
