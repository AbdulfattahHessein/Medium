namespace Medium.BL.Features.Publisher.Responses
{
    public record CreatePublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
