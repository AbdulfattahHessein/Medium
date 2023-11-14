namespace Medium.BL.Features.Publisher.Responses
{
    //public record GetPublisherByIdResponse(int Id, string Name, string? Bio, string? PhotoUrl, int FollowersCount, bool IsFollowing);
    public record GetPublisherByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public int FollowersCount { get; set; }
        public bool IsFollowing { get; set; }

    }
}
