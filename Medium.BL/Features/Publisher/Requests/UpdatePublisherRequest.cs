using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Publisher.Requests
{
    public record UpdatePublisherRequest(int Id, string Name, string? Bio, IFormFile? Photo);
}
