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
using Microsoft.AspNetCore.Http;
using static Medium.BL.ResponseHandler.ApiResponseHandler;

namespace Medium.BL.AppServices
{
    public class PublishersService : AppService, IPublishersService
    {
        public PublishersService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
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
        public async Task<ApiResponse<CreatePublisherResponse>> Create(CreatePublisherRequest request)
        {
            var validator = new CreatePublisherRequestValidator();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            string uploadDirectory = Path.Combine("./Resources", "Photos");
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
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
                return NotFound<GetPublisherByIdResponse>();
            var response = Mapper.Map<GetPublisherByIdResponse>(publisher);
            return Success(response);
        }

        public async Task<ApiResponse<UpdatePublisherResponse>> UpdateAsync(UpdatePublisherRequest request)
        {
            //get the old publisher
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
                return NotFound<UpdatePublisherResponse>();

            //Delete old photo if exist
            if (publisher.PhotoUrl != null)
            {
                File.Delete($@".{publisher.PhotoUrl}");
            }

            //update publisher by map the request to the old publisher
            Mapper.Map(request, publisher);

            //upload the new photo and set the photoUrl 
            var fileName = await UploadFormFileToAsync(request.Photo, Path.Combine("./Resources", "Photos"));
            publisher.PhotoUrl = fileName != null ? $"/Resources/Photos/{fileName}" : null;

            //update the publisher
            UnitOfWork.Publishers.Update(publisher);
            await UnitOfWork.CommitAsync();


            var response = Mapper.Map<UpdatePublisherResponse>(publisher);


            return Success(response);
        }

        public async Task<ApiResponse<DeletePublisherResponse>> DeleteAsync(DeletePublisherRequest request)
        {
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
            {
                return NotFound<DeletePublisherResponse>();
            }
            //Delete publisher photo if exist
            if (publisher.PhotoUrl != null)
            {
                File.Delete($@".{publisher.PhotoUrl}");
            }
            //delete everything related with the publisher like its stories
            //
            //

            UnitOfWork.Publishers.Delete(publisher);
            await UnitOfWork.CommitAsync();

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
            var publishers = await UnitOfWork.Publishers
                .GetAllAsync((request.PageNumber - 1) * request.PageSize, request.PageSize, p => p.Followers, p => p.Followings);
            var publisher = publishers.Find(p => p.Id == request.PublisherId);

            List<Publisher> Followers = new List<Publisher>();
            Followers.AddRange(publisher.Followers);
            List<Publisher> Followings = new List<Publisher>();
            Followings.AddRange(publisher.Followings);
            List<Publisher> FollowersNotFollowing = new List<Publisher>();
            foreach (var Follower in Followers)
            {
                var follower = Followings.Find(f => f.Id == Follower.Id);
                if (follower == null)
                    FollowersNotFollowing.Add(Follower);
            }

            var totalCount = FollowersNotFollowing.Count();

            var response = Mapper.Map<List<FollowerNotFollowingResponse>>(FollowersNotFollowing);

            return Success(response, totalCount, request.PageNumber, request.PageSize);
        }
        public async Task<ApiResponse<AddFollowingResponse>> AddFollowingAsync(AddFollowingRequest request)
        {
            #region Temp
            //var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.PublisherId);
            //var Following = await UnitOfWork.Publishers.GetByIdAsync(request.FollowingId);
            // publisher.Followings.Add(Following);
            //var response = Mapper.Map<AddFollowingResponse>(Following);
            //return Success(response);

            //    var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.PublisherId);
            //    var following = await UnitOfWork.Publishers.GetByIdAsync(request.FollowingId);

            //if (publisher != null && following != null)
            //{
            //    if (publisher.Followings == null )
            //    {
            //        publisher.Followings = new List<Publisher>();
            //    }

            //    publisher.Followings.Add(following);
            //    following.Followers.Add(publisher);
            //   //await UnitOfWork.CommitAsync();
            //    var response = Mapper.Map<AddFollowingResponse>(following);
            //    return Success(response);
            //} 
            #endregion
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.PublisherId);
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

                // Save changes to the database if using Entity Framework or a similar ORM.
                await UnitOfWork.CommitAsync();

                var response = Mapper.Map<AddFollowingResponse>(following);
                return Success(response);
            }

            throw new NotImplementedException();
        }

        public async Task<ApiResponse<DeleteFollowingResponse>> DeleteFollowingAsync(DeleteFollowingRequest request)
        {
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.PublisherId, p => p.Followings, p => p.Followers);
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
