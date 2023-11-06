using AutoMapper;
using FluentValidation;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Medium.BL.AppServices
{
    public abstract class AppService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public int PublisherId => int.Parse(HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public AppService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            HttpContextAccessor = httpContext;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
        protected async Task DoValidationAsync<TValidator, TRequest>(TRequest request, params object[] constructorParameters)
            where TValidator : AbstractValidator<TRequest>
        {
            //TValidator instance;
            //if (unitOfWork != null)
            //{
            //    //object[] constructorParameters = { unitOfWork }; // Pass the parameter values
            //    instance = (TValidator)Activator.CreateInstance(typeof(TValidator), unitOfWork)!;
            //}
            //else instance = Activator.CreateInstance<TValidator>();

            var instance = (TValidator)Activator.CreateInstance(typeof(TValidator), constructorParameters)!;

            var validateResult = await instance.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
        }

    }
}
