//using Medium.Api.Bases;
//using Medium.BL.Features.StoryPhotos.Requests;
//using Medium.BL.Interfaces.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace Medium.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StoryPhotosController : AppControllerBase
//    {
//        private readonly IStoryPhotoServices _storyPhotoServices;

//        public StoryPhotosController(IStoryPhotoServices storyPhotoServices)
//        {
//            _storyPhotoServices = storyPhotoServices;
//        }

//        [HttpPost("CreateStoryPhoto")]
//        public async Task<IActionResult> CreateStoryPhoto([FromForm] CreateStoryPhotoRequest request)
//        {
//            var result = await _storyPhotoServices.Create(request);

//            return ApiResult(result);
//        }
//    }
//}
