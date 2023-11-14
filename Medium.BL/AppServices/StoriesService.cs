using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Stories.Requests;
using Medium.BL.Features.Stories.Responses;
using Medium.BL.Features.Stories.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class StoriesService : AppService, IStoriesService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly string webRootPath;

        public StoriesService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext, IHostingEnvironment hostingEnvironment) : base(unitOfWork, mapper, httpContext)
        {
            this.hostingEnvironment = hostingEnvironment;
            webRootPath = hostingEnvironment.WebRootPath;
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
        public async Task<ApiResponse<CreateStoryResponse>> CreateStoryAsync(CreateStoryRequest request)
        {

            await DoValidationAsync<CreateStoryRequestValidator, CreateStoryRequest>(request, UnitOfWork);

            //var hostingEnvironment = HttpContextAccessor.HttpContext.RequestServices.GetService<IHostingEnvironment>()!;
            //var webRootPath = hostingEnvironment.WebRootPath;



            var storyPhotos = new List<StoryPhoto>();
            var storyVideos = new List<StoryVideo>();

            if (request.StoryPhotos != null)
            {
                var uploadDirectory = Path.Combine(webRootPath, "Resources", "StoryPhotos");
                //string uploadDirectory = Path.Combine("./Resources", "StoryPhotos");

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
                var uploadDirectory = Path.Combine(webRootPath, "Resources", "StoryVideos");
                //string uploadDirectory = Path.Combine("./Resources", "StoryVideos");
                foreach (var file in request.StoryVideos)
                {
                    string? fileName = await UploadFormFileToAsync(file, uploadDirectory);
                    storyVideos.Add(new StoryVideo
                    {
                        Url = $"/Resources/StoryVideos/{fileName}"
                    });
                }
            }
            var publisher = UnitOfWork.Publishers.GetById(PublisherId);
            if (publisher == null)
            {
                return NotFound<CreateStoryResponse>();
            }

            // To Add Topic in Story
            var existTopics = (await UnitOfWork.Topics.GetAllAsync(t => request.Topics.Contains(t.Name)));
            var existTopicsNames = existTopics.Select(t => t.Name).ToList();
            var newTopicsNames = request.Topics.Where(topicName => !existTopicsNames.Contains(topicName)).ToList();
            var newTopics = newTopicsNames.Select(tn => new Topic() { Name = tn });


            var story = new Story
            {
                Title = request.Title,
                Content = request.Content,
                Publisher = publisher,
                StoryPhotos = storyPhotos,
                StoryVideos = storyVideos,

            };
            story.Topics = existTopics;

            foreach (var topic in newTopics)
            {
                story.Topics.Add(topic);
            }
            await UnitOfWork.Stories.InsertAsync(story); await UnitOfWork.CommitAsync();

            var storyMap = Mapper.Map<CreateStoryResponse>(story);
            return Success(storyMap);
        }

        ////// ================================ GETALL ============================================================
        public async Task<ApiResponse<List<GetAllStoryResponse>>> GetAllStories()
        {
            var stories = await UnitOfWork.Stories.GetAllAsync(s => s.StoryPhotos, s => s.StoryVideos, s => s.Publisher);
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

            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id, s => s.StoryPhotos, s => s.StoryVideos, s => s.Topics,
                s => s.Reacts, s => s.Publisher);
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
            //var hostingEnvironment = HttpContextAccessor.HttpContext.RequestServices.GetService<IHostingEnvironment>()!;
            //var webRootPath = hostingEnvironment.WebRootPath;


            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id, s => s.StoryPhotos, s => s.StoryVideos, s => s.Topics);
            if (story == null)
            {
                return NotFound<UpdateStoryResponse>();
            }

            var publisher = UnitOfWork.Publishers.GetById(PublisherId);
            if (publisher == null)
            {
                return NotFound<UpdateStoryResponse>();
            }
            if (publisher == null)
            {
                return NotFound<UpdateStoryResponse>();
            }
            //delete old story photos
            foreach (var photo in story.StoryPhotos)
            {
                File.Delete(Path.GetFullPath(webRootPath + photo.Url));
            }
            //delete old story videos
            foreach (var video in story.StoryVideos)
            {
                File.Delete(Path.GetFullPath(webRootPath + video.Url));
            }

            // Update story photos
            var updatedStoryPhotos = new List<StoryPhoto>();
            if (request.StoryPhotos != null)
            {
                var uploadDirectory = Path.Combine(webRootPath, "Resources", "StoryVideos");

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

            var updatedStoryVideos = new List<StoryVideo>();


            if (request.StoryVideos != null)
            {
                var uploadDirectory = Path.Combine(webRootPath, "Resources", "StoryVideos");

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

            // To Add Topic in Story
            var existTopics = (await UnitOfWork.Topics.GetAllAsync(t => request.Topics.Contains(t.Name)));
            var existTopicsNames = existTopics.Select(t => t.Name).ToList();
            var newTopicsNames = request.Topics.Where(topicName => !existTopicsNames.Contains(topicName)).ToList();
            var newTopics = newTopicsNames.Select(tn => new Topic() { Name = tn });

            story.Topics = existTopics;

            foreach (var topic in newTopics)
            {
                story.Topics.Add(topic);
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
            //var hostingEnvironment = HttpContextAccessor.HttpContext.RequestServices.GetService<IHostingEnvironment>()!;
            //var webRootPath = hostingEnvironment.WebRootPath;

            var story = await UnitOfWork.Stories.GetByIdAsync(request.Id, s => s.StoryPhotos, s => s.StoryVideos);
            if (story == null)
            {
                return NotFound<DeleteStoryResponse>();
            }
            UnitOfWork.Stories.Delete(story);
            //await UnitOfWork.CommitAsync();

            //delete all story photos
            foreach (var photo in story.StoryPhotos)
            {
                File.Delete(Path.GetFullPath(webRootPath + photo.Url));
            }
            //delete all story videos
            foreach (var video in story.StoryVideos)
            {
                File.Delete(Path.GetFullPath(webRootPath + video.Url));
            }

            var storyMap = Mapper.Map<DeleteStoryResponse>(story);
            return Deleted(storyMap);
        }

        ////// ================================ GETALL PAGINATION STORY ============================================================

        public async Task<ApiResponsePaginated<List<GetAllPaginationStoryResponse>>> GetAllAsync(GetAllPaginationStoryRequest request)
        {
            var stories = await UnitOfWork.Stories
                .GetAllAsync(s => s.Title.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize,
                s => s.Publisher, s => s.Topics, s => s.StoryPhotos, s => s.Publisher);

            var totalCount = await UnitOfWork.Stories.CountAsync((s => s.Title.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationStoryResponse>>(stories);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<ApiResponsePaginated<List<GetAllPaginationStoryResponse>>> GetAllPublisherStoriesAsync(GetAllPaginationStoryRequest request)
        {
            var stories = await UnitOfWork.Stories
                 .GetAllAsync(s => s.Title.Contains(request.Search) && s.Publisher.Id == PublisherId, (request.PageNumber - 1) * request.PageSize, request.PageSize,
                 s => s.Publisher, s => s.Topics, s => s.StoryPhotos);

            var totalCount = await UnitOfWork.Stories.CountAsync((s => s.Title.Contains(request.Search)));

            var response = Mapper.Map<List<GetAllPaginationStoryResponse>>(stories);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }

        //public async Task<ApiResponsePaginated<List<GetAllStoriesByTopicNameResponse>>> GetAllStoriesByTopicNameAsync(GetAllStoriesByTopicNameRequest request)
        //{
        //    // var topic = await UnitOfWork.Topics.FirstOrDefaultAsync(t => t.Id == request.TopicId, t => t.Stories);
        //    var topic = await UnitOfWork.Stories.GetAllStoriesByTopicNameAsync(request.TopicId, (request.PageNumber - 1) * request.PageSize, request.PageSize
        //        );
        //    if (topic == null)
        //    {
        //        return new ApiResponsePaginated<List<GetAllStoriesByTopicNameResponse>>
        //        {
        //            StatusCode = System.Net.HttpStatusCode.NotFound,
        //            Message = "Topic Not Found",
        //        };
        //    }

        //    var response = Mapper.Map<List<GetAllStoriesByTopicNameResponse>>(topic);
        //    return Success(response, topic.Count, request.PageNumber, request.PageSize);



        //}
        public async Task<ApiResponsePaginated<List<GetAllStoriesByTopicIdResponse>>> GetAllStoriesByTopicIdAsync(GetAllStoriesByTopicNameRequest request)
        {
            var stories = await UnitOfWork.Stories.GetAllStoriesByTopicIdAsync(request.TopicId, (request.PageNumber - 1) * request.PageSize, request.PageSize);

            if (stories == null || stories.Count == 0)
            {
                return new ApiResponsePaginated<List<GetAllStoriesByTopicIdResponse>>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Topic Not Found",
                };
            }
            var response = Mapper.Map<List<GetAllStoriesByTopicIdResponse>>(stories);
            // Calculate the total number of stories for the given topic
            var totalStories = await UnitOfWork.Stories.CountAsync(s => s.Topics.Any(t => t.Id == request.TopicId));

            foreach (var storyResponse in response)
            {
                storyResponse.StroriesNumber = totalStories;
            }

            return Success(response, totalStories, request.PageNumber, request.PageSize);
        }

    }

}