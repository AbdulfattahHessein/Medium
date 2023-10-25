using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Publisher.Response
{
    public record AddFollowingResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
