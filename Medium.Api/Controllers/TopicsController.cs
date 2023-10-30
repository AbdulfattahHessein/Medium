﻿using Medium.Api.Bases;
using Medium.BL.AppServices;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Topics.Request;
using Medium.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    //    [Route("api/[controller]")]
    //    [ApiController]
    public class TopicsController : AppControllerBase
    {
        private readonly ITopicsService _topicsService;
        public TopicsController(ITopicsService topicsService)
        {
            _topicsService = topicsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _topicsService.GetById(new GetTopicByIdRequest(id));

            return ApiResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicRequest request)
        {
            var result = await _topicsService.CreateAsync(request);

            return ApiResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _topicsService.DeleteAsync(new DeleteTopicRequest(id));

            return ApiResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTopicRequest request)
        {
            var result = await _topicsService.UpdateAsync(request);

            return ApiResult(result);
        }
        [HttpGet("GetAllPaginationTopics")]
        public async Task<IActionResult> GetAllPaginationTopics([FromQuery] GetAllPaginationTopicRequest request)
        {
            var result = await _topicsService.GetAllAsync(request);

            return ApiResult(result);
        }
    }
}
