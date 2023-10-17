using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Publisher.Requests
{
    public record CreatePublisherRequest(string Name, string? Bio, IFormFile? Photo);
}
