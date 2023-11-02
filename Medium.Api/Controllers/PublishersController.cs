using Medium.Api.Bases;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Interfaces.Services;
using Medium.DA.Implementation.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePublisher([FromForm] CreatePublisherRequest request)
        {
            var result = await _publishersService.Create(request);

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
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _publishersService.GetFollowerNotFollowing(request, publisherId);
            return ApiResult(result);
        }
        [HttpPost("Follow")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Follow([FromQuery] AddFollowingRequest request)
        {
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _publishersService.AddFollowingAsync(request, publisherId);


            return ApiResult(result);
        }
        [HttpDelete("UnFollow")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnFollow([FromQuery] DeleteFollowingRequest request)
        {
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _publishersService.DeleteFollowingAsync(request, publisherId);

            return ApiResult(result);
        }
    }
}
