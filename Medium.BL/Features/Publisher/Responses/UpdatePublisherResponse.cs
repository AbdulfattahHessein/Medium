namespace Medium.BL.Features.Publisher.Responses
{
    public record UpdatePublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
