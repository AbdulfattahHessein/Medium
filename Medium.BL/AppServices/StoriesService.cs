using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Features.Stories.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Http;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class StoriesService : AppService, IStoriesService
    {
        public StoriesService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        ////// ================================ CREATE ============================================================
        private async Task<string?> UploadFormFileToAsync(IFormFile? formFile, string uploadDirectory)
        {
            string? fileName = null;
            if (formFile != null && formFile.Length > 0)
            {
                // Ensure the "Photos" directory exists
                Directory.CreateDirectory(uploadDirectory);

                // Generate a unique file name for the uploaded Photo
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var uploadPath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return fileName;
        }



        //========================================= CREATE ========================================
        public async Task<ApiResponse<CreateStoryResponse>> CreateStoryAsync(CreateStoryRequest request, int publisherId)
        {

            await DoValidationAsync<CreateStoryRequestValidator, CreateStoryRequest>(request, UnitOfWork);

            string uploadDirectory = Path.Combine("./Resources", "StoryPhotos");
            // string? fileName = await UploadFormFileToAsync(request.StoryPhotos, uploadDirectory);
            var storyPhotos = new List<StoryPhoto>();
            var storyVideos = new List<StoryVideo>();

            if (request.StoryPhotos != null)
            {
                foreach (var file in request.StoryPhotos)
                {
                    string? fileName = await UploadFormFileToAsync(file, uploadDirectory);
                    storyPhotos.Add(new StoryPhoto
                    {
                        Url = $"/Resources/StoryPhotos/{fileName}"
                    });
                }
            }

            if (request.StoryVideos != null)
            {
                uploadDirectory = Path.Combine("./Resources", "StoryVideos");
                foreach (var file in request.StoryVideos)
                {
                    string? fileName = await UploadFormFileToAsync(file, uploadDirectory);
                    storyVideos.Add(new StoryVideo
                    {
                        Url = $"/Resources/StoryVideos/{fileName}"
                    });
                }
            }

            var publisher = UnitOfWork.Publishers.GetById(publisherId);
            if (publisher == null)
            {
                return NotFound<CreateStoryResponse>();
            }

            // To Add Topic in Story
            var existTopics = (await UnitOfWork.Topics.GetAllAsync(t => request.Topics.Contains(t.Name)));
            var existTopicsNames = existTopics.Select(t => t.Name).ToList();
            var newTopicsNames = request.Topics.Where(topicName => !existTopicsNames.Contains(topicName)).ToList();
            UnitOfWork.Topics.InsertList(newTopicsNames.Select(tn => new Topic() { Name = tn }));
            await UnitOfWork.CommitAsync();


            var story = new Story
            {
                Title = request.Title,
                Content = request.Content,
                Publisher = publisher,
                StoryPhotos = storyPhotos,
                StoryVideos = storyVideos,

            };

            await UnitOfWork.Stories.InsertAsync(story); await UnitOfWork.CommitAsync();
            //await UnitOfWork.StoryPhotos.InsertAsync(storyPhotos); await UnitOfWork.CommitAsync();
            //await UnitOfWork.StoryVideos.InsertAsync(storyVideos); await UnitOfWork.CommitAsync();
            var storyMap = Mapper.Map<CreateStoryResponse>(story);
            return Success(storyMap);
        }

        ////// ================================ GETALL ============================================================
        public async Task<ApiResponse<List<GetAllStoryResponse>>> GetAllStories()
        {
            var stories = await UnitOfWork.Stories.GetAllAsync(s => s.StoryPhotos, s => s.StoryVideos);
            var storiesMap = Mapper.Map<List<GetAllStoryResponse>>(stories);
            return Success(storiesMap);

        }

        ////// ================================ GET BY ID ============================================================

        public async Task<ApiResponse<GetStoryByIdResponse>> GetStoryById(GetStoryByIdRequest request)
        {
            var validator = new GetStoryByIdRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

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
            var stories = await UnitOfWork.Stories.GetAllAsync(story => story.Publisher);
            var response = Mapper.Map<List<GetAllStoryIncludePublisherResponse>>(stories);
            return Success(response);
        }



        ////// ================================ UPDATE ============================================================

        public async Task<ApiResponse<UpdateStoryResponse>> UpdateStory(UpdateStoryRequest request)
        {
            var validator = new UpdateStoryRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id);
            if (story == null)
            {
                return NotFound<UpdateStoryResponse>();
            }

            // Remove old story photos
            //if (story.StoryPhotos != null && story.StoryPhotos.Any())
            //{
            //    foreach (var oldPhoto in story.StoryPhotos)
            //    {
            //        File.Delete($@"./{oldPhoto.Url}");
            //    }
            //}

            string uploadDirectory = Path.Combine("./Resources", "StoryPhotos");
            // string? fileName = await UploadFormFileToAsync(request.StoryPhotos, uploadDirectory);

            // Update story photos
            if (request.StoryPhotos != null)
            {
                var updatedStoryPhotos = new List<StoryPhoto>();
                foreach (var file in request.StoryPhotos)
                {
                    string? fileName = await UploadFormFileToAsync(file, uploadDirectory);
                    updatedStoryPhotos.Add(new StoryPhoto
                    {
                        Url = $"/Resources/StoryPhotos/{fileName}"
                    });
                }
                story.StoryPhotos = updatedStoryPhotos;
            }

            // Update story videos
            if (request.StoryVideos != null)
            {
                var updatedStoryVideos = new List<StoryVideo>();
                uploadDirectory = Path.Combine("./Resources", "StoryVideos");
                foreach (var file in request.StoryVideos)
                {
                    string? fileName = await UploadFormFileToAsync(file, uploadDirectory);
                    updatedStoryVideos.Add(new StoryVideo
                    {
                        Url = $"/Resources/StoryVideos/{fileName}"
                    });
                }
                story.StoryVideos = updatedStoryVideos;
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
            var validator = new DeleteStoryRequestValidator();
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id, s => s.StoryPhotos, s => s.StoryVideos);
            if (story == null)
            {
                return NotFound<DeleteStoryResponse>();
            }
            UnitOfWork.Stories.Delete(story);
            await UnitOfWork.CommitAsync();

            //delete all story photos
            foreach (var photo in story.StoryPhotos)
            {
                File.Delete($@"./{photo.Url}");
            }
            //delete all story videos
            foreach (var video in story.StoryVideos)
            {
                File.Delete($@"./{video.Url}");
            }

            var storyMap = Mapper.Map<DeleteStoryResponse>(story);
            return Deleted(storyMap);
        }

        ////// ================================ GETALL PAGINATION STORY ============================================================

        public async Task<ApiResponsePaginated<List<GetAllPaginationStoryResponse>>> GetAllAsync(GetAllPaginationStoryRequest request)
        {
            var stories = await UnitOfWork.Stories
                .GetAllAsync(s => s.Title.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize,
                s => s.Publisher, s => s.Topics);

            var totalCount = await UnitOfWork.Stories.CountAsync((s => s.Title.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationStoryResponse>>(stories);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }


    }

}