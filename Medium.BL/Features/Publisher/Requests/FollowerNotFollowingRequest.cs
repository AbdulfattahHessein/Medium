using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Publisher.Requests
{
    public record FollowerNotFollowingRequest( int PageNumber = 1, int PageSize = 3 );
}

