//using AutoMapper;
//using Medium.BL.Features.StoryPhotos.Requests;
//using Medium.BL.Features.StoryPhotos.Responses;
//using Medium.BL.Features.StoryPhotos.Validators;
//using Medium.BL.Interfaces.Services;
//using Medium.BL.ResponseHandler;
//using Medium.Core.Entities;
//using Medium.Core.Interfaces.Bases;
//using Microsoft.AspNetCore.Http;
//using System.ComponentModel.DataAnnotations;
//using static Medium.BL.ResponseHandler.ApiResponseHandler;

//namespace Medium.BL.AppServices
//{
//    public class StoryPhotoServices : AppService, IStoryPhotoServices
//    {
//        public StoryPhotoServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
//        {
//        }

//        private async Task<string?> UploadFormFileToAsync(IFormFile? formFile, string uploadDirectory)
//        {
//            string? fileName = null;
//            if (formFile != null && formFile.Length > 0)
//            {
//                // Ensure the "Photos" directory exists
//                Directory.CreateDirectory(uploadDirectory);

//                // Generate a unique file name for the uploaded Photo
//                fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
//                var uploadPath = Path.Combine(uploadDirectory, fileName);

//                using (var stream = new FileStream(uploadPath, FileMode.Create))
//                {
//                    await formFile.CopyToAsync(stream);
//                }
//            }
//            return fileName;
//        }
//        public async Task<ApiResponse<CreateStoryPhotoResponse>> Create(CreateStoryPhotoRequest request)
//        {
//            var validator = new CreateStoryPhotoValidator();
//            var validateResult = validator.Validate(request);
//            if (!validateResult.IsValid)
//            {
//                throw new ValidationException();
//            }
//            var story = UnitOfWork.Stories.GetById(request.StoryId);
//            if (story == null)
//            {
//                return NotFound<CreateStoryPhotoResponse>();
//            }
//            string uploadDirectory = Path.Combine("./Resources", "StoryPhotos");
//            string? fileName = await UploadFormFileToAsync(request.Url, uploadDirectory);

//            var StoryPhoto = new StoryPhoto()
//            {

//                Url = fileName != null ? $"/Resources/StoryPhotos/{fileName}" : null, // Set the relative URL
//                Story = story

//            };

//            await UnitOfWork.StoryPhotos.InsertAsync(StoryPhoto);
//            await UnitOfWork.CommitAsync();

//            var response = Mapper.Map<CreateStoryPhotoResponse>(StoryPhoto);

//            return Success(response);
//        }
//    }
//}
