using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Publisher.Requests
{
    public record AddFollowingRequest(int PublisherId,int FollowingId);
}