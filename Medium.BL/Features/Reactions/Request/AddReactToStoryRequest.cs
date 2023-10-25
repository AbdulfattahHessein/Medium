namespace Medium.BL.Features.Reactions.Request
{
    public record AddReactToStoryRequest(int PublisherId, int ReactionId, int StoryId);

}
