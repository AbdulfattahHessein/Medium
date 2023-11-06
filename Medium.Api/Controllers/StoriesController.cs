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

        //[HttpGet("GetAllStories")]
        ////  [Authorize]
        //public async Task<IActionResult> GetAllStories()
        //{
        //    var stories = await _storiesService.GetAllStories();
        //    return ApiResult(stories);
        //}

        [HttpGet("GetStoryByID/{id}")]
        [Authorize]
        public async Task<IActionResult> GetStoryById(int id)
        {
            var story = await _storiesService.GetStoryById(new GetStoryByIdRequest(id));
            return ApiResult(story);
        }

        //[HttpGet("GetAllStoriesIncludingPublisher")]
        //[Authorize]
        //public async Task<IActionResult> GetAllStoriesIncludingPublisher()
        //{
        //    var stories = await _storiesService.GetAllStoriesIncludingPublisher();
        //    return ApiResult(stories);
        //}

        [HttpGet("GetAllPaginationStories")]
        [Authorize]
        public async Task<IActionResult> GetAllPaginationStories([FromQuery] GetAllPaginationStoryRequest request)
        {
            var result = await _storiesService.GetAllAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetAllPublisherStories")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllPublisherStories([FromQuery] GetAllPaginationStoryRequest request)
        {
            //

            var result = await _storiesService.GetAllPublisherStoriesAsync(request);

            return ApiResult(result);
        }

        [HttpPost("CreateStory")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateStory([FromForm] CreateStoryRequest request)
        {
            //
            //// var topics = request.Topics; // Access the Topics from the request
            var result = await _storiesService.CreateStoryAsync(request);

            return ApiResult(result);
        }


        [HttpPut("UpdateStory")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateStory([FromForm] UpdateStoryRequest request)
        {


            var story = await _storiesService.UpdateStory(request);
            return ApiResult(story);
        }

        [HttpDelete("DeleteStory/{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var story = await _storiesService.DeleteStoryAsync(new DeleteStoryRequest(id));
            return ApiResult(story);
        }
    }
}
