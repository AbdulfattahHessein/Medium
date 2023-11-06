namespace Medium.BL.Features.Stories.Responses
{
    //public record CreateStoryResponse(int Id, string Title, string Content, DateTime CreationDate, int PublisherId, List<string>? StoryPhotos, List<string>? StoryVideos, List<string>? Topics);
    public record CreateStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int PublisherId { get; set; }
        public List<string>? StoryPhotos { get; set; }
        public List<string>? StoryVideos { get; set; }
        public List<string>? Topics { get; set; }
    }
}