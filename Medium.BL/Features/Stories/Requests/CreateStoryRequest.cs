﻿using Microsoft.AspNetCore.Http;

namespace Medium.BL.Features.Stories.Requests
{
    //  public record CreateStoryRequest(string Title, string Content, int PublisherId, List<IFormFile>? StoryPhotos, List<IFormFile>? StoryVideos);
    public record CreateStoryRequest(string Title, string Content, List<IFormFile>? StoryPhotos, List<IFormFile>? StoryVideos, List<string> Topics);
}

