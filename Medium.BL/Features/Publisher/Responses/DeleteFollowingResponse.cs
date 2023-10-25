using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Publisher.Responses
{
    public record DeleteFollowingResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
