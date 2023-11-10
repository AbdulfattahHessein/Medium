namespace Medium.BL.Features.Stories.Responses
{
    //public record GetAllPaginationStoryResponse(int Id, string Title, string Content, DateTime CreationDate, string PublisherName, string PublisherPhotoUrl, List<string> Name)
    //{

    //};

    public record GetAllPaginationStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PublisherId { get; set; }
        public DateTime CreationDate { get; set; }
        public string PublisherName { get; set; }
        public string PublisherPhotoUrl { get; set; }
        public string StoryMainPhoto { get; set; }
        public List<string> Topics { get; set; }

    }
}


