using FluentValidation;
using Medium.BL.Features.Publisher.Requests;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Publisher.Validators
{
    public class CreatePublisherRequestValidator : AbstractValidator<CreatePublisherRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePublisherRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(p => p.Bio).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName} Must be not empty");

            //RuleFor(p => p.Photo).NotNull().
            //    WithMessage("You Must Upload Your Photo");
        }

    }
}
