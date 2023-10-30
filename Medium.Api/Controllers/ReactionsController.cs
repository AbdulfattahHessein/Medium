using Medium.Api.Bases;
using Medium.BL.Features.Reactions.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Medium.Api.Controllers
{
    //    [Route("api/[controller]")]
    //    [ApiController]
    public class ReactionsController : AppControllerBase
    {
        private readonly IReactionsService _reactionsService;

        public ReactionsController(IReactionsService reactionsService)
        {
            _reactionsService = reactionsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _reactionsService.GetById(new GetReactionByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateReactionRequest request)
        {
            var result = await _reactionsService.CreateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reactionsService.DeleteAsync(new DeleteReactionRequest(id));

            return ApiResult(result);
        }
        [HttpPut]

        public async Task<IActionResult> Update(UpdateReactionRequest request)
        {
            var result = await _reactionsService.UpdateAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetAllPaginationReactions")]
        public async Task<IActionResult> GetAllPaginationReactions([FromQuery] GetAllPaginationReactionsRequest request)
        {
            var result = await _reactionsService.GetAllAsync(request);

            return ApiResult(result);
        }
        [HttpPost("React")]
        [Authorize]
        public async Task<IActionResult> AddReactToStory(AddReactToStoryRequest request)
        {
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _reactionsService.AddReactToStory(request, publisherId);
            return NoContent();
        }
        [HttpDelete("React")]
        [Authorize]
        public async Task<IActionResult> RemoveReactFromStory(RemoveReactFromStoryRequest request)
        {
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _reactionsService.RemoveReactFromStory(request, publisherId);

            return ApiResult(result);
        }
    }
}
