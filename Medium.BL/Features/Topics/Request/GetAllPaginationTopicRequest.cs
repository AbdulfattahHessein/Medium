using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Topics.Request
{
    public record GetAllPaginationTopicRequest(int PageNumber = 1, int PageSize = 10, string Search = "");
}
