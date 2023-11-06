using Medium.Api.Bases;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class TopicsController : AppControllerBase
    {
        private readonly ITopicsService _topicsService;
        public TopicsController(ITopicsService topicsService)
        {
            _topicsService = topicsService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _topicsService.GetById(new GetTopicByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTopicRequest request)
        {
            var result = await _topicsService.CreateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _topicsService.DeleteAsync(new DeleteTopicRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateTopicRequest request)
        {
            var result = await _topicsService.UpdateAsync(request);

            return ApiResult(result);
        }
        [HttpGet("GetAllPaginationTopics")]
        [Authorize]
        public async Task<IActionResult> GetAllPaginationTopics([FromQuery] GetAllPaginationTopicRequest request)
        {
            var result = await _topicsService.GetAllAsync(request);

            return ApiResult(result);
        }
    }
}
