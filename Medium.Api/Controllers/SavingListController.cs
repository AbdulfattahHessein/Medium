using Medium.Api.Bases;
using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Medium.Api.Controllers
{
    //  [Route("api/[controller]")]
    //  [ApiController]
    public class SavingListController : AppControllerBase
    {
        private readonly ISavingListServices _savingListServices;

        public SavingListController(ISavingListServices savingListServices)
        {
            _savingListServices = savingListServices;
        }


        [HttpGet("GetAllSaveingList")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSaveingList()
        {
            var saveList = await _savingListServices.GetAllAsync();
            return ApiResult(saveList);
        }

        [HttpGet("GetAllPaginationSaveList")]
        public async Task<IActionResult> GetAllPaginationStoies([FromQuery] GetAllPaginationSaveListRequest request)
        {
            var result = await _savingListServices.GetAllPaginationAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetSavingListById/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetSavingListById(int id)
        {
            var result = await _savingListServices.GetByIdAsync(new GetSavingListByIdRequest(id));

            return ApiResult(result);
        }

        [HttpPost("CreateSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateSavingList([FromForm] CreateSavingListRequest request)
        {
            var publisherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _savingListServices.CreateAsync(request, publisherId);

            return ApiResult(result);
        }

        [HttpPut("UpdateSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateSavingList([FromForm] UpdateSavingListRequest request)
        {
            var saveList = await _savingListServices.UpdateAsync(request);
            return ApiResult(saveList);
        }


        [HttpDelete("DeleteSaveList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteSaveList([FromQuery] DeleteSavingListRequest request)
        {
            var saveList = await _savingListServices.DeleteAsync(request);
            return ApiResult(saveList);
        }

        [HttpPost("AddStoryToSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddStoryToSavingList([FromBody] AddStoryToSaveListRequest request)
        {
            var result = await _savingListServices.AddStoryToSaveList(request);
            return ApiResult(result);
        }

        [HttpPost("RemoveStoryFromSavingList")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveStoryFromSavingList([FromBody] RemoveStoryFromSavingListRequest request)
        {
            var result = await _savingListServices.RemoveStoryFromSavingList(request);
            return ApiResult(result);
        }

        //    [HttpGet("GetAllSavingListsWithStories")]
        //    public async Task<IActionResult> GetAllSavingListsWithStories()
        //    {
        //        var result = await _savingListServices.GetAllSavingListsWithStoriesAsync();
        //        return ApiResult(result);
        //    }
        //}
    }
}

