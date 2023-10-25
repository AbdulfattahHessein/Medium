﻿namespace Medium.BL.Features.Stories.Responses
{
    public record GetAllStoryResponse(int Id, string Title, string Content, DateTime CreationDate, List<string> StoryPhotos, List<string> StoryVideos);

}
