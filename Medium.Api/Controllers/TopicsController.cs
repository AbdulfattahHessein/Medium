using Medium.Api.Bases;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    //    [Route("api/[controller]")]
    //    [ApiController]
    public class TopicsController : AppControllerBase
    {
        private readonly ITopicsService _TopicsService;

        public TopicsController(ITopicsService TopicsService)
        {
            _TopicsService = TopicsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _TopicsService.GetById(new GetTopicByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateTopicRequest request)
        {
            var result = await _TopicsService.CreateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _TopicsService.DeleteAsync(new DeleteTopicRequest(id));

            return ApiResult(result);
        }
        [HttpPut]

        public async Task<IActionResult> Update(UpdateTopicRequest request)
        {
            var result = await _TopicsService.UpdateAsync(request);

            return ApiResult(result);
        }
    }
}
