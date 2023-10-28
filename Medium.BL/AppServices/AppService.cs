using AutoMapper;
using FluentValidation;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.AppServices
{
    public abstract class AppService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public AppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
        protected async Task DoValidationAsync<TValidator, TRequest>(TRequest request, IUnitOfWork unitOfWork)
            where TValidator : AbstractValidator<TRequest>
        {
            //TValidator instance;
            //if (unitOfWork != null)
            //{
            //    //object[] constructorParameters = { unitOfWork }; // Pass the parameter values
            //    instance = (TValidator)Activator.CreateInstance(typeof(TValidator), unitOfWork)!;
            //}
            //else instance = Activator.CreateInstance<TValidator>();

            var instance = (TValidator)Activator.CreateInstance(typeof(TValidator), unitOfWork)!;

            var validateResult = await instance.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
        }

    }
}
