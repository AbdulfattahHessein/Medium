using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Publisher.Responses
{
    public record UpdatePublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
