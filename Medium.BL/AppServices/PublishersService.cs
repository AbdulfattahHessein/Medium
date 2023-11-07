using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Response;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Features.Publisher.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class PublishersService : AppService, IPublishersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly string webRootPath;
        public PublishersService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext, IHostingEnvironment hostingEnvironment) : base(unitOfWork, mapper, httpContext)
        {
            _userManager = HttpContextAccessor.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>()!;
            this.hostingEnvironment = hostingEnvironment;
            webRootPath = hostingEnvironment.WebRootPath;
        }

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

        //we dont need this function, because publisher are created when user created
        public async Task<ApiResponse<CreatePublisherResponse>> Create(CreatePublisherRequest request)
        {
            await DoValidationAsync<CreatePublisherRequestValidator, CreatePublisherRequest>(request, UnitOfWork);

            string uploadDirectory = Path.Combine(webRootPath, "Resources", "Photos");
            string? fileName = await UploadFormFileToAsync(request.Photo, uploadDirectory);

            var publisher = new Publisher(request.Name)
            {
                Bio = request.Bio,
                PhotoUrl = fileName != null ? $"/Resources/Photos/{fileName}" : null // Set the relative URL
            };

            await UnitOfWork.Publishers.InsertAsync(publisher);
            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<CreatePublisherResponse>(publisher);

            return Success(response);
        }

        public async Task<ApiResponse<GetPublisherByIdResponse>> GetById(GetPublisherByIdRequest request)
        {
            var validator = new GetPublisherByIdRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id, s => s.Followers);
            if (publisher == null)
                return NotFound<GetPublisherByIdResponse>();
            var response = Mapper.Map<GetPublisherByIdResponse>(publisher);
            return Success(response);
        }

        public async Task<ApiResponse<UpdatePublisherResponse>> UpdateAsync(UpdatePublisherRequest request)
        {
            var validator = new UpdatePublisherRequestValidator(UnitOfWork);
            var validateResult = validator.Validate(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            //get the old publisher
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
                return NotFound<UpdatePublisherResponse>();

            //Delete publisher photo if exist
            if (publisher.PhotoUrl != null)
            {
                File.Delete(Path.GetFullPath(webRootPath + publisher.PhotoUrl));
            }

            //update publisher by map the request to the old publisher
            Mapper.Map(request, publisher);

            //upload the new photo and set the photoUrl 
            var fileName = await UploadFormFileToAsync(request.Photo, Path.Combine(webRootPath, "Resources", "Photos"));
            publisher.PhotoUrl = fileName != null ? $"/Resources/Photos/{fileName}" : null;

            //update the publisher
            UnitOfWork.Publishers.Update(publisher);
            await UnitOfWork.CommitAsync();


            var response = Mapper.Map<UpdatePublisherResponse>(publisher);


            return Success(response);
        }

        //we dont need this function, and we need to create api to remove user and all thing related to its publisher
        public async Task<ApiResponse<DeletePublisherResponse>> DeleteAsync(DeletePublisherRequest request)
        {
            await DoValidationAsync<DeletePublisherRequestValidator, DeletePublisherRequest>(request);

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
            {
                return NotFound<DeletePublisherResponse>();
            }
            //Delete publisher photo if exist
            if (publisher.PhotoUrl != null)
            {
                File.Delete(Path.GetFullPath(webRootPath + publisher.PhotoUrl));
            }
            //delete everything related with the publisher like its stories
            //when delete the user, publisher will also deleted
            var user = await _userManager.FindByIdAsync(publisher.Id.ToString());
            await _userManager.DeleteAsync(user);

            var response = Mapper.Map<DeletePublisherResponse>(publisher);

            return Deleted(response);
        }

        public async Task<ApiResponsePaginated<List<GetAllPublisherResponse>>> GetAllAsync(GetAllPublisherRequest request)
        {

            var publishers = await UnitOfWork.Publishers
                .GetAllAsync(p => p.Name.Contains(request.Search), (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var totalCount = await UnitOfWork.Publishers.CountAsync(p => p.Name.Contains(request.Search));

            var response = Mapper.Map<List<GetAllPublisherResponse>>(publishers);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }


        public async Task<ApiResponsePaginated<List<FollowerNotFollowingResponse>>> GetFollowerNotFollowing(FollowerNotFollowingRequest request)
        {
            await DoValidationAsync<FollowerNotFollowingRequestValidator, FollowerNotFollowingRequest>(request, UnitOfWork);

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId, p => p.Followers, p => p.Followings)!;

            if (publisher == null)
            {
                return new ApiResponsePaginated<List<FollowerNotFollowingResponse>>()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }

            var FollowersNotFollowing = publisher.Followers!.Except(publisher.Followings!);

            var response = Mapper.Map<List<FollowerNotFollowingResponse>>(FollowersNotFollowing);

            return Success(response, FollowersNotFollowing.Count(), request.PageNumber, request.PageSize);
        }
        public async Task<ApiResponse<AddFollowingResponse>> AddFollowingAsync(AddFollowingRequest request)
        {
            await DoValidationAsync<AddFollowingRequestValidator, AddFollowingRequest>(request, UnitOfWork);
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId);
            var following = await UnitOfWork.Publishers.GetByIdAsync(request.FollowingId);

            if (publisher != null && following != null)
            {
                if (publisher.Followings == null)
                {
                    publisher.Followings = new List<Publisher>();
                }

                if (following.Followers == null)
                {
                    following.Followers = new List<Publisher>();
                }

                publisher.Followings.Add(following);
                following.Followers.Add(publisher);

                await UnitOfWork.CommitAsync();

                var response = Mapper.Map<AddFollowingResponse>(following);
                return Success(response);
            }

            throw new NotImplementedException();
        }

        public async Task<ApiResponse<DeleteFollowingResponse>> DeleteFollowingAsync(DeleteFollowingRequest request)
        {

            await DoValidationAsync<DeleteFollowingRequestValidator, DeleteFollowingRequest>(request, UnitOfWork);
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId, p => p.Followings, p => p.Followers);
            var following = await UnitOfWork.Publishers.GetByIdAsync(request.FollowingId, p => p.Followings, p => p.Followers);

            if (publisher != null && following != null)
            {

                if (publisher.Followings == null)
                {
                    publisher.Followings = new List<Publisher>();

                }

                if (following.Followers == null)
                {
                    following.Followers = new List<Publisher>();
                }

                publisher.Followings.Remove(following);
                following.Followers.Remove(publisher);

                await UnitOfWork.CommitAsync();

                var response = Mapper.Map<DeleteFollowingResponse>(following);
                return Success(response);

            }
            throw new NotImplementedException();
        }
    }
}
