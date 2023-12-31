﻿using AutoMapper;
using FluentValidation;
using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Features.SavingLists.Response;
using Medium.BL.Features.SavingLists.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Http;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class SavingListServices : AppService, ISavingListServices
    {
        public SavingListServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : base(unitOfWork, mapper, httpContext)
        {
        }


        //========================================= Add Story To SaveList =====================================
        public async Task<ApiResponse<AddStoryToSaveListResponse>> AddStoryToSaveList(AddStoryToSaveListRequest request)
        {
            var validator = new AddStoryToSaveListRequestValidator();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var story = await UnitOfWork.Stories.GetByIdAsync(request.StoryId);

            if (story == null)
            {
                return NotFound<AddStoryToSaveListResponse>();
            }
            // اي دا يا ايمن
            //var saveListOld = await UnitOfWork.SavingLists.GetByIdAsync(request.saveListId, sv => sv.Stories);

            var saveList = await UnitOfWork.SavingLists.GetFirstAsync(s => s.Id == request.SaveListId && s.PublisherId == PublisherId, s => s.Stories);

            if (saveList == null)
            {
                return NotFound<AddStoryToSaveListResponse>();
            }
            //if (saveList.Stories == saveListOld.Stories)
            //{
            //    return BadRequest<AddStoryToSaveListResponse>("This Story is already Saved in this SaveList");
            //}

            //Ensure the saving list Stories collection is not null.
            //if (saveList.Stories == null)
            //{
            //    saveList.Stories = new List<Story>();
            //}
            if (saveList.Stories.Contains(story))
            {
                return BadRequest<AddStoryToSaveListResponse>("This Story is already Saved in this SaveList");
            }

            saveList.Stories.Add(story);
            // Save changes to the database.
            await UnitOfWork.CommitAsync();
            // Create the response with the name and stories.
            var responseMap = Mapper.Map<AddStoryToSaveListResponse>(saveList);
            return Success(responseMap);
        }

        //========================================= Remove Story From SavingList =====================================
        public async Task<ApiResponse<RemoveStoryFromSavingListResponse>> RemoveStoryFromSavingList(RemoveStoryFromSavingListRequest request)
        {
            var validator = new RemoveStoryFromSavingListRequestValidator(UnitOfWork);
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var story = await UnitOfWork.Stories.GetByIdAsync(request.StoryId);
            if (story == null)
            {
                return NotFound<RemoveStoryFromSavingListResponse>();
            }
            var savingList = await UnitOfWork.SavingLists.GetByIdAsync(request.SavingListId, sv => sv.Stories);
            if (savingList == null)
            {
                return NotFound<RemoveStoryFromSavingListResponse>();
            }

            // Ensure the saving list Stories collection is not null
            //if (savingList.Stories == null)
            //{
            //    savingList.Stories = new List<Story>();
            //}
            // Remove the story from the saving list.
            savingList.Stories.Remove(story);
            await UnitOfWork.CommitAsync();
            var responseMap = Mapper.Map<RemoveStoryFromSavingListResponse>(savingList);
            return Success(responseMap);
        }


        //========================================= Create SaveList =====================================
        public async Task<ApiResponse<CreateSavingListResponse>> CreateAsync(CreateSavingListRequest requset)
        {
            var validator = new CreateSavingListRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(requset);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            //var publisher = UnitOfWork.Publishers.GetById(requset.PublisherId);
            //if (publisher == null)
            //{
            //    return NotFound<CreateSavingListResponse>();
            //}
            // await DoValidationAsync<CreateSavingListRequestValidator, CreateSavingListRequest>(requset, UnitOfWork);


            var savingList = new SavingList()
            {
                Name = requset.Name,
                PublisherId = PublisherId,

            };
            await UnitOfWork.SavingLists.InsertAsync(savingList);
            await UnitOfWork.CommitAsync();
            var savingListMap = Mapper.Map<CreateSavingListResponse>(savingList);
            return Success(savingListMap);
        }
        //========================================= Delete SaveList =====================================
        public async Task<ApiResponse<DeleteSavingListResponse>> DeleteAsync(DeleteSavingListRequest requset)
        {
            var validator = new DeleteSavingListRequestValidator();
            var validateResult = validator.Validate(requset);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            //  await DoValidationAsync<DeleteSavingListRequestValidator, DeleteSavingListRequest>(requset, UnitOfWork);

            var saveList = await UnitOfWork.SavingLists.GetByIdAsync(requset.Id);
            if (saveList == null)
            {
                return NotFound<DeleteSavingListResponse>();
            }

            UnitOfWork.SavingLists.Delete(saveList);
            await UnitOfWork.CommitAsync();
            var responseMap = Mapper.Map<DeleteSavingListResponse>(saveList);
            return Success(responseMap);

        }
        //========================================= GetAll SaveList =====================================
        public async Task<ApiResponse<List<GetAllSavingListResponse>>> GetAllAsync()
        {

            var saveList = await UnitOfWork.SavingLists.GetAllAsync(savingList => savingList.Publisher);
            var saveListMapp = Mapper.Map<List<GetAllSavingListResponse>>(saveList);
            return Success(saveListMapp);



        }
        //========================================= GetById SaveList =====================================
        public async Task<ApiResponse<GetSavingListByIdResponse>> GetByIdAsync(GetSavingListByIdRequest requset)
        {
            await DoValidationAsync<GetSavingListByIdRequestValidator, GetSavingListByIdRequest>(requset, UnitOfWork);


            var saveList = await UnitOfWork.SavingLists.GetByIdAsync(requset.Id);
            if (saveList == null)
            {
                return NotFound<GetSavingListByIdResponse>();
            }

            var responseMap = Mapper.Map<GetSavingListByIdResponse>(saveList);
            return Success(responseMap);
        }

        //========================================= Update SaveList =====================================
        public async Task<ApiResponse<UpdateSavingListResponse>> UpdateAsync(UpdateSavingListRequest requset)
        {
            await DoValidationAsync<UpdateSavingListValidator, UpdateSavingListRequest>(requset, UnitOfWork);


            var saveList = await UnitOfWork.SavingLists.GetByIdAsync(requset.Id);
            if (saveList == null)
            {
                return NotFound<UpdateSavingListResponse>();
            }
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(requset.PublisherId);
            if (publisher == null)
            {
                return NotFound<UpdateSavingListResponse>();
            }
            Mapper.Map(requset, saveList);
            UnitOfWork.SavingLists.Update(saveList);
            await UnitOfWork.CommitAsync();
            var saveListMap = Mapper.Map<UpdateSavingListResponse>(saveList);
            return Success(saveListMap);
        }


        ////// ================================ GETALL PAGINATION SaveLIst ============================================================

        public async Task<ApiResponsePaginated<List<GetAllPaginationSaveListResponse>>> GetAllPaginationAsync(GetAllPaginationSaveListRequest request)
        {
            var saveLists = await UnitOfWork.SavingLists
                .GetAllAsync(s => s.Name.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize,
                s => s.Publisher);

            var totalCount = await UnitOfWork.SavingLists.CountAsync((s => s.Name.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationSaveListResponse>>(saveLists);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }
        public async Task<ApiResponsePaginated<List<GetAllPaginationSaveListResponse>>> GetAllPublisherSaveListsAsync(GetAllPaginationSaveListRequest request)
        {
            var saveLists = await UnitOfWork.SavingLists
                .GetAllAsync(s => s.Name.Contains(request.Search) && s.Publisher.Id == PublisherId, (request.PageNumber - 1) * request.PageSize, request.PageSize,
                s => s.Publisher);

            var totalCount = await UnitOfWork.SavingLists.CountAsync((s => s.Name.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationSaveListResponse>>(saveLists);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }

    }
}
