namespace Medium.BL.Features.Stories.Responses
{
    //public record GetAllStoryIncludePublisherResponse(int Id, string Title, string Content, DateTime CreationDate, string PublisherName);
    public record GetAllStoryIncludePublisherResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string PublisherName { get; set; }

    }
}
