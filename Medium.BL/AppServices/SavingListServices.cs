using AutoMapper;
using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Features.SavingLists.Response;
using Medium.BL.Features.SavingLists.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System.ComponentModel.DataAnnotations;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class SavingListServices : AppService, ISavingListServices
    {

        public SavingListServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ApiResponse<AddStoryToSaveListResponse>> AddStoryToSaveList(AddStoryToSaveListRequest requset)
        {
            var story = UnitOfWork.Stories.GetById(requset.storyId);
            if (story == null)
            {
                return NotFound<AddStoryToSaveListResponse>();
            }
            var saveList = UnitOfWork.SavingLists.GetById(requset.saveListId);
            if (saveList == null)
            {
                return NotFound<AddStoryToSaveListResponse>();
            }

            if (saveList.Stories == null)
            {
                saveList.Stories = new List<Story>();
            }
            saveList.Stories.Add(story);
            UnitOfWork.CommitAsync();
            var responseMap = Mapper.Map<AddStoryToSaveListResponse>(saveList);
            return Success(responseMap);
        }

        public async Task<ApiResponse<RemoveStoryFromSavingListResponse>> RemoveStoryFromSavingList(RemoveStoryFromSavingListRequest request)
        {
            var savingList = await UnitOfWork.SavingLists.GetByIdAsync(request.SavingListId);
            if (savingList == null)
            {
                return NotFound<RemoveStoryFromSavingListResponse>();
            }

            var story = await UnitOfWork.Stories.GetByIdAsync(request.StoryId);
            if (story == null)
            {
                return NotFound<RemoveStoryFromSavingListResponse>();
            }

            if (savingList.Stories == null)
            {
                savingList.Stories = new List<Story>();
            }
            savingList.Stories.Remove(story);
            await UnitOfWork.CommitAsync();

            var responseMap = Mapper.Map<RemoveStoryFromSavingListResponse>(savingList);
            return Success(responseMap);
        }


        public async Task<ApiResponse<CreateSavingListResponse>> CreateAsync(CreateSavingListRequest requset)
        {
            var validator = new CreateSavingListValidator();
            var validateResult = validator.Validate(requset);
            if (!validateResult.IsValid)
            {
                throw new ValidationException();
            }

            var publisher = UnitOfWork.Publishers.GetById(requset.PublisherId);
            if (publisher == null)
            {
                return NotFound<CreateSavingListResponse>();
            }

            var savingList = new SavingList()
            {
                Name = requset.Name,
                Publisher = publisher

            };

            await UnitOfWork.SavingLists.InsertAsync(savingList);
            await UnitOfWork.CommitAsync();
            var savingListMap = Mapper.Map<CreateSavingListResponse>(savingList);
            return Success(savingListMap);
        }

        public async Task<ApiResponse<DeleteSavingListResponse>> DeleteAsync(DeleteSavingListRequest requset)
        {
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

        public async Task<ApiResponse<List<GetAllSavingListResponse>>> GetAllAsync()
        {

            var saveList = await UnitOfWork.SavingLists.GetAllAsync(savingList => savingList.Publisher);
            var saveListMapp = Mapper.Map<List<GetAllSavingListResponse>>(saveList);
            return Success(saveListMapp);



        }

        public async Task<ApiResponse<GetSavingListByIdResponse>> GetByIdAsync(GetSavingListByIdRequest requset)
        {
            var saveList = await UnitOfWork.SavingLists.GetByIdAsync(requset.Id);
            if (saveList == null)
            {
                return NotFound<GetSavingListByIdResponse>();
            }

            var responseMap = Mapper.Map<GetSavingListByIdResponse>(saveList);
            return Success(responseMap);
        }


        public async Task<ApiResponse<UpdateSavingListResponse>> UpdateAsync(UpdateSavingListRequest requset)
        {

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
    }
}
