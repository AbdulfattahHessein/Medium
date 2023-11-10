using Medium.Api.Bases;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class PublishersController : AppControllerBase
    {
        private readonly IPublishersService _publishersService;

        public PublishersController(IPublishersService publishersService)
        {
            this._publishersService = publishersService;

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPublisherRequest request)
        {
            var result = await _publishersService.GetAllAsync(request);

            return ApiResult(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var result = await _publishersService.GetById(new GetPublisherByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdatePublisher([FromForm] UpdatePublisherRequest request)
        {
            var result = await _publishersService.UpdateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publishersService.DeleteAsync(new DeletePublisherRequest(id));

            return ApiResult(result);
        }
        [HttpGet("GetFollowerNotFollowing")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetFollowerNotFollowing([FromQuery] FollowerNotFollowingRequest request)
        {

            var result = await _publishersService.GetFollowerNotFollowing(request);
            return ApiResult(result);
        }
        [HttpPost("Follow")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Follow([FromQuery] AddFollowingRequest request)
        {


            var result = await _publishersService.AddFollowingAsync(request);


            return ApiResult(result);
        }
        [HttpDelete("UnFollow")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnFollow([FromQuery] DeleteFollowingRequest request)
        {

            var result = await _publishersService.DeleteFollowingAsync(request);

            return ApiResult(result);
        }
        [HttpGet("Followers")]
        [Authorize]
        public async Task<IActionResult> GetAllFollowers([FromQuery] GetAllFollowersRequest request)
        {

            var result = await _publishersService.GetAllFollowers(request);

            return ApiResult(result);
        }
    }
}
