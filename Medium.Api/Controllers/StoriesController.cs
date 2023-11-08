using Medium.Api.Bases;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class StoriesController : AppControllerBase
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetStoryById(int id)
        {
            var story = await _storiesService.GetStoryById(new GetStoryByIdRequest(id));
            return ApiResult(story);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStories([FromQuery] GetAllPaginationStoryRequest request)
        {
            var result = await _storiesService.GetAllAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetAllPublisherStories")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllPublisherStories([FromQuery] GetAllPaginationStoryRequest request)
        {
            var result = await _storiesService.GetAllPublisherStoriesAsync(request);

            return ApiResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateStory([FromForm] CreateStoryRequest request)
        {
            var result = await _storiesService.CreateStoryAsync(request);

            return ApiResult(result);
        }


        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateStory([FromForm] UpdateStoryRequest request)
        {


            var story = await _storiesService.UpdateStory(request);
            return ApiResult(story);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var story = await _storiesService.DeleteStoryAsync(new DeleteStoryRequest(id));
            return ApiResult(story);
        }
    }
}
