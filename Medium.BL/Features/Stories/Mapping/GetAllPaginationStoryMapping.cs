﻿using Medium.BL.Features.Stories.Responses;
using Medium.Core.Entities;

namespace Medium.BL.Features.Stories.Mapping
{
    public partial class StoryProfile
    {
        void GetAllPaginationStoryMapping()
        {
            CreateMap<Story, GetAllPaginationStoryResponse>();
        }
    }
}
