namespace Medium.BL.Features.Publisher.Responses
{
    public record GetPublisherByIdResponse(int Id, string Name, string? Bio, string? PhotoUrl, int FollowersCount);
}
