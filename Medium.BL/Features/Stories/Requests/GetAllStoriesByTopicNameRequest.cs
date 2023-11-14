namespace Medium.BL.Features.Stories.Requests
{
    public record GetAllStoriesByTopicNameRequest(int TopicId, int PageNumber = 1, int PageSize = 3);
}
