using Medium.Api.Bases;
using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> GetAllSaveingList()
        {
            var saveList = await _savingListServices.GetAllAsync();
            return ApiResult(saveList);
        }
        [HttpPost("CreateSavingList")]
        public async Task<IActionResult> CreateSavingList([FromForm] CreateSavingListRequest request)
        {
            var result = await _savingListServices.CreateAsync(request);

            return ApiResult(result);
        }

        [HttpGet("GetSavingListById/{id}")]
        public async Task<IActionResult> GetSavingListById(int id)
        {
            var result = await _savingListServices.GetByIdAsync(new GetSavingListByIdRequest(id));

            return ApiResult(result);
        }

        [HttpPut("UpdateSavingList")]
        public async Task<IActionResult> UpdateSavingList([FromForm] UpdateSavingListRequest request)
        {
            var saveList = await _savingListServices.UpdateAsync(request);
            return ApiResult(saveList);
        }


        [HttpDelete("DeleteSaveList/{id}")]
        public async Task<IActionResult> DeleteSaveList(int id)
        {
            var saveList = await _savingListServices.DeleteAsync(new DeleteSavingListRequest(id));
            return ApiResult(saveList);
        }

        [HttpPost("AddStoryToSavingList")]
        public async Task<IActionResult> AddStoryToSavingList([FromBody] AddStoryToSaveListRequest request)
        {
            var result = await _savingListServices.AddStoryToSaveList(request);
            return ApiResult(result);
        }

        [HttpPost("RemoveStoryFromSavingList")]
        public async Task<IActionResult> RemoveStoryFromSavingList([FromBody] RemoveStoryFromSavingListRequest request)
        {
            var result = await _savingListServices.RemoveStoryFromSavingList(request);
            return ApiResult(result);
        }
    }
}

