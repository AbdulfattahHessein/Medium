using Medium.Api.Bases;
using Medium.BL.Features.Reactions.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class ReactionsController : AppControllerBase
    {
        private readonly IReactionsService _reactionsService;

        public ReactionsController(IReactionsService reactionsService)
        {
            _reactionsService = reactionsService;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _reactionsService.GetById(new GetReactionByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(CreateReactionRequest request)
        {
            var result = await _reactionsService.CreateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reactionsService.DeleteAsync(new DeleteReactionRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateReactionRequest request)
        {
            var result = await _reactionsService.UpdateAsync(request);

            return ApiResult(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReactions([FromQuery] GetAllPaginationReactionsRequest request)
        {
            var result = await _reactionsService.GetAllAsync(request);

            return ApiResult(result);
        }
        [HttpPost("React")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReactToStory(AddReactToStoryRequest request)
        {
            //
            await _reactionsService.AddReactToStory(request);
            return NoContent();
        }
        [HttpDelete("React")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveReactFromStory(RemoveReactFromStoryRequest request)
        {
            //

            var result = await _reactionsService.RemoveReactFromStory(request);

            return ApiResult(result);
        }
    }
}
