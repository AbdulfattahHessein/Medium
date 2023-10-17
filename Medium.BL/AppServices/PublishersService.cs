using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.BL.Features.Publisher.Responses;
using Medium.BL.Features.Publisher.Validators;
using Medium.BL.Features.Stories.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<ApiResponse<CreatePublisherResponse>> CreatePublisherAsync(CreatePublisherRequest request)
        {
            var validator = new CreatePublisherRequestValidator();
            var validateResult = validator.Validate(request);
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

        public async Task<ApiResponse<GetPublisherByIdResponse>> GetPublisherById(GetPublisherByIdRequest request)
        {
            var publisher = await UnitOfWork.Publishers.GetByIdAsync(request.Id);
            if (publisher == null)
                return NotFound<GetPublisherByIdResponse>();
            var response = Mapper.Map<GetPublisherByIdResponse>(publisher);
            return Success(response);

        }

        public async Task<ApiResponse<UpdatePublisherResponse>> UpdatePublisherAsync(UpdatePublisherRequest request)
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

        public async Task<ApiResponse<DeletePublisherResponse>> DeletePublisherAsync(DeletePublisherRequest request)
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
    }
}
