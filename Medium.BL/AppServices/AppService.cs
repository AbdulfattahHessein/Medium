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
        protected async Task DoValidationAsync<T, TRequest>(TRequest request, IUnitOfWork? unitOfWork = null)
            where T : AbstractValidator<TRequest>
        {
            T instance;
            if (unitOfWork != null)
            {
                //object[] constructorParameters = { unitOfWork }; // Pass the parameter values

                instance = (T)Activator.CreateInstance(typeof(T), unitOfWork)!;

            }
            else instance = Activator.CreateInstance<T>();

            var validateResult = await instance.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
        }

    }
}
