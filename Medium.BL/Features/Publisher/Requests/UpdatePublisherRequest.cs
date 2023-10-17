using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Publisher.Requests
{
    public record UpdatePublisherRequest(int Id, string Name, string? Bio, IFormFile? Photo);
}
