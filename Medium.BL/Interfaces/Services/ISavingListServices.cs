﻿using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Features.SavingLists.Response;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface ISavingListServices
    {
        Task<ApiResponse<CreateSavingListResponse>> CreateAsync(CreateSavingListRequest requset);
        //Task<ApiResponse<List<GetSavingListWithStoriesResponse>>> GetAllSavingListsWithStoriesAsync();
        Task<ApiResponse<List<GetAllSavingListResponse>>> GetAllAsync();
        Task<ApiResponse<GetSavingListByIdResponse>> GetByIdAsync(GetSavingListByIdRequest requset);
        Task<ApiResponse<UpdateSavingListResponse>> UpdateAsync(UpdateSavingListRequest requset);
        Task<ApiResponse<DeleteSavingListResponse>> DeleteAsync(DeleteSavingListRequest requset);
        Task<ApiResponse<AddStoryToSaveListResponse>> AddStoryToSaveList(AddStoryToSaveListRequest requset);
        Task<ApiResponse<RemoveStoryFromSavingListResponse>> RemoveStoryFromSavingList(RemoveStoryFromSavingListRequest request);
        Task<ApiResponsePaginated<List<GetAllPaginationSaveListResponse>>> GetAllPaginationAsync(GetAllPaginationSaveListRequest request);
        Task<ApiResponsePaginated<List<GetAllPaginationSaveListResponse>>> GetAllPublisherSaveListsAsync(GetAllPaginationSaveListRequest request);

    }
}
