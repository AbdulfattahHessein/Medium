using Medium.Api.Bases;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
