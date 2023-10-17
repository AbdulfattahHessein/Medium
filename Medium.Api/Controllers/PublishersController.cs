using Medium.Api.Bases;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;
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
        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromForm] CreatePublisherRequest request)
        {
            var result = await _publishersService.CreatePublisherAsync(request);

            return ApiResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var result = await _publishersService.GetPublisherById(new GetPublisherByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePublisher([FromForm] UpdatePublisherRequest request)
        {
            var result = await _publishersService.UpdatePublisherAsync(request);

            return ApiResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publishersService.DeletePublisherAsync(new DeletePublisherRequest(id));

            return ApiResult(result);
        }

    }
}
