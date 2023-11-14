using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Response;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Features.Publisher.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Constants;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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

            var publisher = new Publisher
            {
                Name = request.Name,
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
            await DoValidationAsync<GetPublisherByIdRequestValidator, GetPublisherByIdRequest>(request, UnitOfWork);

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id, s => s.Followers);
            if (publisher == null)
                return NotFound<GetPublisherByIdResponse>();
            var response = Mapper.Map<GetPublisherByIdResponse>(publisher);
            return Success(response);
        }

        public async Task<ApiResponse<UpdatePublisherResponse>> UpdateAsync(UpdatePublisherRequest request)
        {
            await DoValidationAsync<UpdatePublisherRequestValidator, UpdatePublisherRequest>(request, UnitOfWork);

            //get the old publisher
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
                return NotFound<UpdatePublisherResponse>();

            //Delete publisher photo if exist
            if (publisher.PhotoUrl != null && publisher.PhotoUrl != Defaults.ProfilePhotoPath)
            {
                File.Delete(Path.GetFullPath(webRootPath + publisher.PhotoUrl));
            }

            //update publisher by map the request to the old publisher
            Mapper.Map(request, publisher);

            //upload the new photo and set the photoUrl 
            var fileName = await UploadFormFileToAsync(request.Photo, Path.Combine(webRootPath, "Resources", "Photos"));
            publisher.PhotoUrl = fileName != null ? $"/Resources/Photos/{fileName}" : Defaults.ProfilePhotoPath;

            //update the publisher
            UnitOfWork.Publishers.Update(publisher);
            await UnitOfWork.CommitAsync();


            var response = Mapper.Map<UpdatePublisherResponse>(publisher);


            return Success(response);
        }

        //we dont need this function, and we need to create api to remove user and all thing related to its publisher
        public async Task<ApiResponse<DeletePublisherResponse>> DeleteAsync(DeletePublisherRequest request)
        {
            await DoValidationAsync<DeletePublisherRequestValidator, DeletePublisherRequest>(request, UnitOfWork);

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
            {
                return NotFound<DeletePublisherResponse>("Publisher Not Found");
            }
            //Delete publisher photo if exist
            if (publisher.PhotoUrl != null && publisher.PhotoUrl != Defaults.ProfilePhotoPath)
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

            #region old code
            //var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId, p => p.Followers, p => p.Followings)!;

            //if (publisher == null)
            //{
            //    return new ApiResponsePaginated<List<FollowerNotFollowingResponse>>()
            //    {
            //        StatusCode = System.Net.HttpStatusCode.NotFound,
            //    };
            //}

            //var FollowersNotFollowing = publisher.Followers!.Except(publisher.Followings!);

            //var response = Mapper.Map<List<FollowerNotFollowingResponse>>(FollowersNotFollowing);
            #endregion

            var FollowersNotFollowing = await UnitOfWork.Publishers.GetFollowersNotFollowings(PublisherId, (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var response = Mapper.Map<List<FollowerNotFollowingResponse>>(FollowersNotFollowing);

            return Success(response, FollowersNotFollowing.Count, request.PageNumber, request.PageSize);
        }
        public async Task<ApiResponse<AddFollowingResponse>> AddFollowingAsync(AddFollowingRequest request)
        {
            await DoValidationAsync<AddFollowingRequestValidator, AddFollowingRequest>(request, UnitOfWork);

            if (request.FollowingId == PublisherId)
                return BadRequest<AddFollowingResponse>("You can't follow or unfollow yourself");

            var followingExist = await UnitOfWork.Publishers.AnyAsync(p => p.Id == request.FollowingId);

            if (!followingExist)
                return NotFound<AddFollowingResponse>("Publisher you want to follow Not Found");

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId, p => p.Followings);

            var following = publisher?.Followings?.Where(f => f.Id == request.FollowingId).FirstOrDefault();

            if (following != null)
                return BadRequest<AddFollowingResponse>("You already follow this publisher");

            following = new Publisher { Id = request.FollowingId };

            var entityEntry = UnitOfWork.Attach(following);

            publisher?.Followings?.Add(following);

            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<AddFollowingResponse>(following);

            return Success(response, "followed successfully");

            #region another code for better performance
            //var publisher = new Publisher() { Id = PublisherId };

            //UnitOfWork.Attach(publisher);

            //var followingExist = await UnitOfWork.Publishers.AnyAsync(p => p.Id == request.FollowingId);

            //if (!followingExist)
            //    return NotFound<AddFollowingResponse>();

            //var following = new Publisher() { Id = request.FollowingId };

            //UnitOfWork.Attach(following);

            //publisher.Followings!.Add(following);

            //await UnitOfWork.CommitAsync();

            //var response = Mapper.Map<AddFollowingResponse>(following);

            //return Success(response);
            #endregion
        }

        public async Task<ApiResponse<DeleteFollowingResponse>> DeleteFollowingAsync(DeleteFollowingRequest request)
        {

            await DoValidationAsync<DeleteFollowingRequestValidator, DeleteFollowingRequest>(request, UnitOfWork);

            if (request.FollowingId == PublisherId)
                return BadRequest<DeleteFollowingResponse>("You can't follow or unfollow yourself");

            var followingExist = await UnitOfWork.Publishers.AnyAsync(p => p.Id == request.FollowingId);

            if (!followingExist)
                return NotFound<DeleteFollowingResponse>("Publisher you want to unfollow Not Found");

            var publisher = await UnitOfWork.Publishers.GetByIdAsync(PublisherId, p => p.Followings);

            var following = publisher.Followings.Where(f => f.Id == request.FollowingId).FirstOrDefault();

            if (following == null)
                return BadRequest<DeleteFollowingResponse>("You already not follow this publisher");

            publisher?.Followings?.Remove(following);

            await UnitOfWork.CommitAsync();

            var response = Mapper.Map<DeleteFollowingResponse>(following);

            return Success(response, "Unfollowed successfully");
        }

        public async Task<ApiResponsePaginated<List<GetAllFollowersResponse>>> GetAllFollowers(GetAllFollowersRequest request)
        {
            var followers = await UnitOfWork.Publishers.GetAllFollowers(request.PublisherId, (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var response = Mapper.Map<List<GetAllFollowersResponse>>(followers);

            return Success(response, followers.Count, request.PageNumber, request.PageSize);


        }

        public async Task<ApiResponsePaginated<List<GetAllFollowersResponse>>> GetAllFolloweings(GetAllFollowersRequest request)
        {
            var followers = await UnitOfWork.Publishers.GetAllFollowings(request.PublisherId, (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var response = Mapper.Map<List<GetAllFollowersResponse>>(followers);

            return Success(response, followers.Count, request.PageNumber, request.PageSize);


        }
    }
}
