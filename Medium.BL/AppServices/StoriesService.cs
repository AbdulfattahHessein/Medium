using AutoMapper;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Features.Stories.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using System.ComponentModel.DataAnnotations;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class StoriesService : AppService, IStoriesService
    {
        public StoriesService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        ////// ================================ CREATE ============================================================

        public async Task<ApiResponse<CreateStoryResponse>> CreateStoryAsync(CreateStoryRequest request)
        {
            var validator = new CreateStoryRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException();
            }

            var publisher = UnitOfWork.Publishers.GetById(request.PublisherId);
            if (publisher == null)
            {
                return NotFound<CreateStoryResponse>();
            }
            var story = new Story()
            {
                Title = request.Title,
                Content = request.Content,
                Publisher = publisher
            };
            await UnitOfWork.Stories.InsertAsync(story);
            await UnitOfWork.CommitAsync();

            var storyMap = Mapper.Map<CreateStoryResponse>(story);
            return Success(storyMap);
        }

        ////// ================================ GETALL ============================================================
        public async Task<ApiResponse<List<GetAllStoryResponse>>> GetAllStories()
        {
            var stories = await UnitOfWork.Stories.GETALL();
            var storiesMap = Mapper.Map<List<GetAllStoryResponse>>(stories);
            return Success(storiesMap);

        }

        ////// ================================ GET BY ID ============================================================

        public async Task<ApiResponse<GetStoryByIdResponse>> GetStoryById(GetStoryByIdRequest request)
        {
            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id);
            if (story == null)
            {
                return NotFound<GetStoryByIdResponse>();
            }
            var response = Mapper.Map<GetStoryByIdResponse>(story);
            return Success(response);

        }


        ////// ================================ GETALL INCLUDE PUBLISHER ============================================================

        public async Task<ApiResponse<List<GetAllStoryIncludePublisherResponse>>> GetAllStoriesIncludingPublisher()
        {
            var stories = UnitOfWork.Stories.GetStoriesIncludingPublisher(story => story.Publisher);
            var response = Mapper.Map<List<GetAllStoryIncludePublisherResponse>>(stories);
            return Success(response);
        }



        ////// ================================ UPDATE ============================================================

        public async Task<ApiResponse<UpdateStoryResponse>> UpdateStory(UpdateStoryRequest request)
        {
            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id);
            if (story == null)
            {
                return NotFound<UpdateStoryResponse>();
            }
            Mapper.Map(request, story);
            UnitOfWork.Stories.Update(story);
            await UnitOfWork.CommitAsync();
            var storyMap = Mapper.Map<UpdateStoryResponse>(story);
            return Success(storyMap);
        }

        ////// ================================ DELETE ============================================================

        public async Task<ApiResponse<DeleteStoryResponse>> DeleteStoryAsync(DeleteStoryRequest request)
        {
            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id);
            if (story == null)
            {
                return NotFound<DeleteStoryResponse>();
            }
            UnitOfWork.Stories.Delete(story);
            await UnitOfWork.CommitAsync();
            var storyMap = Mapper.Map<DeleteStoryResponse>(story);
            return Deleted(storyMap);
        }


    }

}