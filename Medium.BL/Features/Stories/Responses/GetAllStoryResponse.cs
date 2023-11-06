namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string>? StoryPhotos { get; set; }
        public List<string>? StoryVideos { get; set; }
    }

}
