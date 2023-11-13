namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllStoriesByTopicNameResponse
    {
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public string PublisherPhoto { get; set; }
        public int StroriesNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string>? StoryPhotos { get; set; }
        public List<string>? StoryVideos { get; set; }
    }
}
