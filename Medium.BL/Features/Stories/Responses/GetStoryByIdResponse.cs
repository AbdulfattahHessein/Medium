namespace Medium.BL.Features.Stories.Responses
{
    public record GetStoryByIdResponse(int Id, string Title, string Content, DateTime CreationDate);

}
