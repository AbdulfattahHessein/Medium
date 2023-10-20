using Medium.Api.Bases;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api")]
    //[ApiController]
    public class StoriesController : AppControllerBase
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        [HttpGet("GetAllStories")]

        public async Task<IActionResult> GetAllStories()
        {
            var stories = await _storiesService.GetAllStories();
            return ApiResult(stories);
        }

        [HttpGet("GetStoryByID/{id}")]
        public async Task<IActionResult> GetStoryById(int id)
        {
            var story = await _storiesService.GetStoryById(new GetStoryByIdRequest(id));
            return ApiResult(story);
        }

        [HttpGet("GetAllStoriesIncludingPublisher")]
        public async Task<IActionResult> GetAllStoriesIncludingPublisher()
        {
            var stories = await _storiesService.GetAllStoriesIncludingPublisher();
            return ApiResult(stories);
        }

        [HttpPost("CreateStory")]
        public async Task<IActionResult> CreateStory([FromForm] CreateStoryRequest request)
        {
            var result = await _storiesService.CreateStoryAsync(request);

            return ApiResult(result);
        }


        [HttpPut("UpdateStory")]
        public async Task<IActionResult> UpdateStory([FromForm] UpdateStoryRequest request)
        {
            var story = await _storiesService.UpdateStory(request);
            return ApiResult(story);
        }

        [HttpDelete("DeleteStory/{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var story = await _storiesService.DeleteStoryAsync(new DeleteStoryRequest(id));
            return ApiResult(story);
        }
    }
}
