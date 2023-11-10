namespace Medium.BL.Features.Stories.Responses
{
    // public record GetStoryByIdResponse(int Id, string Title, string Content, DateTime CreationDate, List<string>? StoryPhotos, List<string>? StoryVideos, List<string> Topics);
    public record GetStoryByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherPhoto { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string>? StoryPhotos { get; set; }
        public List<string>? StoryVideos { get; set; }
        public List<string>? Topics { get; set; }
        public int ReactsCount { get; set; }



    }
}
