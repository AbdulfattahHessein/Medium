namespace Medium.BL.Features.Stories.Responses
{
    //public record DeleteStoryResponse(int Id, string Title, string Content, DateTime CreationDate);
    public record DeleteStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
