using Medium.Api.Bases;
using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class SavingListController : AppControllerBase
    {
        private readonly ISavingListServices _savingListServices;

        public SavingListController(ISavingListServices savingListServices)
        {
            _savingListServices = savingListServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStories([FromQuery] GetAllPaginationSaveListRequest request)
        {
            var result = await _savingListServices.GetAllPaginationAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetAllPublisherSaveLists")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllPublisherSaveLists([FromQuery] GetAllPaginationSaveListRequest request)
        {
            //
            var result = await _savingListServices.GetAllPublisherSaveListsAsync(request);

            return ApiResult(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetSavingListById(int id)
        {
            var result = await _savingListServices.GetByIdAsync(new GetSavingListByIdRequest(id));

            return ApiResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateSavingList([FromForm] CreateSavingListRequest request)
        {

            var result = await _savingListServices.CreateAsync(request);

            return ApiResult(result);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateSavingList([FromForm] UpdateSavingListRequest request)
        {
            var saveList = await _savingListServices.UpdateAsync(request);
            return ApiResult(saveList);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteSaveList(int id)
        {
            var saveList = await _savingListServices.DeleteAsync(new DeleteSavingListRequest(id));
            return ApiResult(saveList);
        }

        [HttpPost("AddStoryToSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddStoryToSavingList([FromQuery] AddStoryToSaveListRequest request)
        {
            var result = await _savingListServices.AddStoryToSaveList(request);
            return ApiResult(result);
        }

        [HttpPost("RemoveStoryFromSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveStoryFromSavingList([FromQuery] RemoveStoryFromSavingListRequest request)
        {
            var result = await _savingListServices.RemoveStoryFromSavingList(request);
            return ApiResult(result);
        }
    }
}

