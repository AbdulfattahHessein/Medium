namespace Medium.BL.Features.Publisher.Responses
{
    public record DeletePublisherResponse(int Id, string Name, string? Bio, string? PhotoUrl);
}
