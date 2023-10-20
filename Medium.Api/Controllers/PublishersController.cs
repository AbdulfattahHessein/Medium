using Medium.Api.Bases;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class PublishersController : AppControllerBase
    {
        private readonly IPublishersService _publishersService;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public PublishersController(IPublishersService publishersService, IWebHostEnvironment hostingEnvironment)
        {
            this._publishersService = publishersService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPublisherRequest request)
        {
            var result = await _publishersService.GetAllAsync(request);

            return ApiResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromForm] CreatePublisherRequest request)
        {
            var result = await _publishersService.Create(request);

            return ApiResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var result = await _publishersService.GetById(new GetPublisherByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePublisher([FromForm] UpdatePublisherRequest request)
        {
            var result = await _publishersService.UpdateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publishersService.DeleteAsync(new DeletePublisherRequest(id));

            return ApiResult(result);
        }

    }
}
