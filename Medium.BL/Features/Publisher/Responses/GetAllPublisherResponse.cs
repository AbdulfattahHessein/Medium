namespace Medium.BL.Features.Publisher.Response
{
    public record GetAllPublisherResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsFollowing { get; set; }
    }

}
